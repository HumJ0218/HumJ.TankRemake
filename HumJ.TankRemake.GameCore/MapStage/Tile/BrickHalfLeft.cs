namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickHalfLeft(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 2, 2, 2, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
