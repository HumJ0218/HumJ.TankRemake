using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public abstract class TileBase(int gridX, int gridY, int textureIndexY, int textureIndexX, int textureCount, int textureVariant, bool anime) : IHaveTexture, IHaveHitBox
    {
        public const int GridSize = 16;

        public abstract TileLayer Layer { get; }

        public Texture? this[int tick] => Texture.Back[textureIndexY][textureIndexX + ((anime ? tick / 20 : 0) + textureVariant) % textureCount];

        public Vector2 Position { get; } = new(gridX * GridSize, gridY * GridSize);
        public Vector2 GridPosition { get; } = new(gridX, gridY);

        public RectangleF HitBox { get; } = new(gridX * GridSize, gridY * GridSize, GridSize, GridSize);

        public virtual bool HitWith(TankBase tank)
        {
            HitWithTank = HitBox.IntersectsWith(tank.HitBox);
            return HitWithTank;
        }

        public bool HitWithTank { get; private set; }
    }
}