namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickHalfBottom(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 2, 0, 2, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
