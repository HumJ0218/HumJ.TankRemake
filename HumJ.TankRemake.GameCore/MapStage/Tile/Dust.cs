using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Dust(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 0, 12, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Cover;

        public override bool HitWith(TankBase tank) => false;
    }
}
