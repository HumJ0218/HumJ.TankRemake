namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickHalfLeft(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
