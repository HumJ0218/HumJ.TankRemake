namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickQuarterBottomRight(int gridX, int gridY) : TileBase(gridX, gridY, 2, 6, 1, 0, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
