using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Bush(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 0, 8, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Cover;

        public override bool HitWith(TankBase tank) => false;
    }
}
