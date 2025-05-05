using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.MapStage.Tile;
using HumJ.TankRemake.GameCore.Weapon;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Tank
{
    public abstract class TankBase(ILogger<TankBase> logger) : ICanGoTick, IHaveControl<TankControl>, IHaveHitBox
    {
        public const int GridSize = 16;
        public static readonly RectangleF MoveBound = new(GridSize, GridSize, GridSize * 38, GridSize * 28);

        public bool this[TankControl control]
        {
            get => this.control[control];
            set
            {
                this.control[control] = value;

                if (value)
                {
                    switch (control)
                    {
                        case TankControl.Up: Direction = EntityDirection.Up; break;
                        case TankControl.Right: Direction = EntityDirection.Right; break;
                        case TankControl.Down: Direction = EntityDirection.Down; break;
                        case TankControl.Left: Direction = EntityDirection.Left; break;
                        case TankControl.PrimaryFire: break;
                        case TankControl.SecondaryFire: break;
                    }
                }
            }
        }

        public Vector2 Position { get; protected set; }
        public EntityDirection Direction { get; protected set; }
        public Vector2 Speed { get; protected set; }
        public RectangleF HitBox => new(Position.X - GridSize, Position.Y - GridSize, GridSize * 2, GridSize * 2);
        public abstract Camp Camp { get; }

        public bool HitWithBullet { get; private set; }

        public abstract bool Injured { get; }
        public abstract TankType Type { get; }
        public virtual WeaponBase PrimaryWeapon { get; protected set; }
        public virtual WeaponBase SecondaryWeapon { get; protected set; }

        public abstract int MaxHealthCount { get; }
        public abstract int MaxAmmoCount { get; }
        public abstract int MaxSpeedLevel { get; }
        public abstract int MaxPowerLevel { get; }

        public int Score { get; private set; }
        public int Life { get; private set; }

        public abstract int HealthCount { get; }
        public abstract int AmmoCount { get; }
        public abstract int SpeedLevel { get; }
        public abstract int PowerLevel { get; }
        public abstract EnhanceType Enhance { get; }

        private readonly Dictionary<TankControl, bool> control = new()
        {
            { TankControl.Up, false },
            { TankControl.Left, false },
            { TankControl.Right, false },
            { TankControl.Down, false },
            { TankControl.PrimaryFire, false },
            { TankControl.SecondaryFire, false }
        };

        public void GoTick(Playground playground)
        {
            logger.LogDebug($"{nameof(GoTick)}");

            ProcessMove(playground);
            ProcessFire(playground);
        }

        private void ProcessMove(Playground playground)
        {
            logger.LogDebug($"{nameof(ProcessMove)}");

            var oldPosition = Position;
            var oldSpeed = Speed;
            TryMove(playground);

            // 起步对齐网格
            if (oldSpeed == Vector2.Zero && Speed != Vector2.Zero)
            {
                var gridX = (int)Math.Round(Position.X / GridSize);
                var gridY = (int)Math.Round(Position.Y / GridSize);

                if (Speed.X != 0 && Speed.Y == 0) Position = new Vector2(Position.X, gridY * GridSize);
                if (Speed.Y != 0 && Speed.X == 0) Position = new Vector2(gridX * GridSize, Position.Y);
            }

            // 处理与方块碰撞
            {
                var hits = playground.BackgroundTile.Concat(playground.ForegroundTile).Where(tile => tile.HitWith(this)).ToArray();
                if (hits.Length > 0)
                {
                    if (Speed.X != 0)
                    {
                        var oldGridX = (int)Math.Round(oldPosition.X / GridSize);
                        Position = new(oldGridX * GridSize, Position.Y);
                        Speed = new(0, Speed.Y);
                    }

                    if (Speed.Y != 0)
                    {
                        var oldGridY = (int)Math.Round(oldPosition.Y / GridSize);
                        Position = new(Position.X, oldGridY * GridSize);
                        Speed = new(Speed.X, 0);
                    }
                }
            }
        }

        private void ProcessFire(Playground playground)
        {
            if (this[TankControl.PrimaryFire])
            {
                var bullets = PrimaryWeapon.Fire(this, playground);
                foreach (var bullet in bullets)
                {
                    playground.Bullet.Add(bullet);
                }
            }

            if (this[TankControl.SecondaryFire])
            {
                var bullets = SecondaryWeapon.Fire(this, playground);
                foreach (var bullet in bullets)
                {
                    playground.Bullet.Add(bullet);
                }
            }
        }

        public virtual bool HitWith(BulletBase bullet)
        {
            HitWithBullet = HitBox.IntersectsWith(bullet.HitBox) && bullet.Camp != Camp;
            return HitWithBullet;
        }

        void TryMove(Playground playground)
        {
            var ice = playground.BackgroundTile.Where(m => m is Ice).Cast<Ice>().Where(m => m.OnIce(this)).ToArray();
            var onIce = ice.Length > 2;


            // 动力
            var speedUp = false;
            {
                var power = (float)(SpeedLevel * 2 * (onIce ? 0.1 : 1));
                if (this[TankControl.Up] && (Speed == Vector2.Zero || (Speed.Y < 0 && Direction == EntityDirection.Up))) { Speed += new Vector2(0, -1) * power; speedUp = true; }
                if (this[TankControl.Right] && (Speed == Vector2.Zero || (Speed.X > 0 && Direction == EntityDirection.Right))) { Speed += new Vector2(1, 0) * power; speedUp = true; }
                if (this[TankControl.Down] && (Speed == Vector2.Zero || (Speed.Y > 0 && Direction == EntityDirection.Down))) { Speed += new Vector2(0, 1) * power; speedUp = true; }
                if (this[TankControl.Left] && (Speed == Vector2.Zero || (Speed.X < 0 && Direction == EntityDirection.Left))) { Speed += new Vector2(-1, 0) * power; speedUp = true; }
            }

            // 阻力
            if (!speedUp)
            {
                var speedLength = Speed.Length();
                if (speedLength > 0)
                {
                    var resistance = (float)(onIce ? 0.25 : 4);
                    var newSpeedLength = speedLength - resistance;
                    if (newSpeedLength < 0) newSpeedLength = 0;
                    Speed = Vector2.Normalize(Speed) * newSpeedLength;
                }
            }

            // 限制速度
            var maxSpeed = 2.6f + 0.6f * SpeedLevel;
            if (Speed.Length() > maxSpeed) Speed = Vector2.Normalize(Speed) * maxSpeed;

            // 计算移动
            Position += Speed;

            // 限制边界位置
            if (Position.X < MoveBound.X)
            {
                Position = new Vector2(MoveBound.X, Position.Y);
                Speed = new(0, Speed.Y);
            }
            if (Position.Y < MoveBound.Y)
            {
                Position = new Vector2(Position.X, MoveBound.Y);
                Speed = new(Speed.X, 0);
            }
            if (Position.X > MoveBound.Right)
            {
                Position = new Vector2(MoveBound.Right, Position.Y);
                Speed = new(0, Speed.Y);
            }
            if (Position.Y > MoveBound.Bottom)
            {
                Position = new Vector2(Position.X, MoveBound.Bottom);
                Speed = new(Speed.X, 0);
            }
        }
    }
}
