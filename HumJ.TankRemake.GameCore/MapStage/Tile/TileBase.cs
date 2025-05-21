using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using HumJ.TankRemake.GameCore.Weapon;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public abstract class TileBase(int gridX, int gridY, int variant) : IHaveHitBox
    {
        public const int GridSize = 16;

        public abstract TileLayer Layer { get; }

        public int Variant { get; } = variant;

        public Vector2 Position { get; } = new(gridX * GridSize, gridY * GridSize);
        public Vector2 GridPosition { get; } = new(gridX, gridY);

        public RectangleF HitBox { get; } = new(gridX * GridSize, gridY * GridSize, GridSize, GridSize);

        public bool HitWithTank { get; private set; }
        public bool HitWithBullet { get; private set; }

        public virtual bool HitWith(TankBase tank)
        {
            HitWithTank = HitBox.IntersectsWith(tank.HitBox);
            return HitWithTank;
        }

        public bool HitWith(BulletBase bullet)
        {
            HitWithBullet = HitBox.IntersectsWith(bullet.HitBox);

            return HitWithBullet;
        }

        public virtual (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet) => (0, this);
    }
}