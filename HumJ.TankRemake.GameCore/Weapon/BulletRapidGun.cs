using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class BulletRapidGun(Vector2 position, EntityDirection direction, Vector2 speed, Camp camp) : BulletBase(position, direction, speed, 60, camp)
    {
        public override WeaponType Type => WeaponType.Rapid;

        protected override IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; } = new Dictionary<EntityDirection, RectangleF>
        {
            [EntityDirection.Up] = new RectangleF(-2, -8, 4, 16),
            [EntityDirection.Right] = new RectangleF(-8, -2, 16, 4),
            [EntityDirection.Down] = new RectangleF(-2, -8, 4, 16),
            [EntityDirection.Left] = new RectangleF(-8, -2, 16, 4),
        };
    }
}
