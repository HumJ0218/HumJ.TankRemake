using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class BulletFireGun(Vector2 position, EntityDirection direction, Vector2 speed, Camp camp) : BulletBase(position, direction, speed, 40, camp)
    {
        public override WeaponType Type => WeaponType.Fire;

        protected override IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; } = new Dictionary<EntityDirection, RectangleF>
        {
            [EntityDirection.Up] = new RectangleF(-6, -8, 12, 16),
            [EntityDirection.Right] = new RectangleF(-8, -6, 16, 12),
            [EntityDirection.Down] = new RectangleF(-6, -8, 12, 16),
            [EntityDirection.Left] = new RectangleF(-8, -6, 16, 12),
        };
    }
}
