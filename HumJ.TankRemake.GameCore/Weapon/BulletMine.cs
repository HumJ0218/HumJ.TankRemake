using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class BulletMine(Vector2 position, EntityDirection direction, Vector2 speed, Camp camp) : BulletBase(position, direction, speed, int.MaxValue, camp)
    {
        public override WeaponType Type => WeaponType.Mine;

        protected override IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; } = new Dictionary<EntityDirection, RectangleF>
        {
            [EntityDirection.Up] = new RectangleF(-8, -8, 16, 16),
            [EntityDirection.Right] = new RectangleF(-8, -8, 16, 16),
            [EntityDirection.Down] = new RectangleF(-8, -8, 16, 16),
            [EntityDirection.Left] = new RectangleF(-8, -8, 16, 16),
        };
    }
}
