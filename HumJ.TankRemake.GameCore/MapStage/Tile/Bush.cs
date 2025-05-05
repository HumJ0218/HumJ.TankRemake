using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Bush(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Cover;

        public override bool HitWith(TankBase tank) => false;
    }
}
