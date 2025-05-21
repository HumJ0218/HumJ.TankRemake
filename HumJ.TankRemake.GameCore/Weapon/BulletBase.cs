using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.MapStage.Tile;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public abstract class BulletBase(Vector2 position, EntityDirection direction, Vector2 speed, int ttl, Camp camp, int damage = 1) : ICanGoTick, IHaveHitBox
    {
        public Camp Camp { get; } = camp;
        public Vector2 Position { get; private set; } = position;
        public EntityDirection Direction { get; } = direction;
        public Vector2 Speed { get; } = speed;
        public int TimeToLive { get; private set; } = ttl;
        public int Damage { get; protected set; } = damage;

        public RectangleF HitBox => new(HitBoxByDirection[Direction].Location + new SizeF(Position), HitBoxByDirection[Direction].Size);
        public abstract WeaponType Type { get; }

        protected abstract IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; }

        public void GoTick(Playground playground)
        {
            Position += Speed;
            TimeToLive--;
        }

        public IReadOnlyDictionary<TileBase, TileBase?> HitWith(TileBase[] hits)
        {
            var damage = 0;
            var tileChanged = new Dictionary<TileBase, TileBase?>();

            foreach (var tile in hits)
            {
                var (d, t) = tile.DamageBy(this);
                damage = int.Max(damage, d);

                if (t != tile)
                {
                    tileChanged[tile] = t;
                }
            }

            Damage -= damage;
            if (Damage < 1)
            {
                TimeToLive = 0;
            }

            return tileChanged;
        }
    }
}