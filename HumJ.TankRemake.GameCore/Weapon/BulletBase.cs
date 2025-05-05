using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public abstract class BulletBase(Vector2 position, EntityDirection direction, Vector2 speed, int ttl, Camp camp) : ICanGoTick, IHaveHitBox
    {
        public Camp Camp { get; } = camp;
        public Vector2 Position { get; private set; } = position;
        public EntityDirection Direction { get; } = direction;
        public Vector2 Speed { get; } = speed;
        public int TimeToLive { get; private set; } = ttl;

        public RectangleF HitBox => new(HitBoxByDirection[Direction].Location + new SizeF(Position), HitBoxByDirection[Direction].Size);
        public abstract WeaponType Type { get; }

        protected abstract IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; }

        public void GoTick(Playground playground)
        {
            Position += Speed;
            TimeToLive--;
        }
    }
}