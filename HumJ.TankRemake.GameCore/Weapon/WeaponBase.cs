using HumJ.TankRemake.GameCore.Tank;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public abstract class WeaponBase
    {
        protected WeaponBase()
        {
            CurrentAmmo = ClipAmmo;
        }

        public abstract WeaponType Type { get; }
        public abstract int ClipAmmo { get; }
        public virtual int MaxAmmo => ClipAmmo * 5;
        public int CurrentAmmo { get; protected set; }

        public abstract BulletBase[] Fire(TankBase tank, Playground playground);

        protected static Vector2 BulletStartPosition(TankBase tank) => tank.Position + tank.Direction switch
        {
            EntityDirection.Up => new Vector2(0, -16),
            EntityDirection.Right => new Vector2(16, 0),
            EntityDirection.Down => new Vector2(0, 16),
            EntityDirection.Left => new Vector2(-16, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}