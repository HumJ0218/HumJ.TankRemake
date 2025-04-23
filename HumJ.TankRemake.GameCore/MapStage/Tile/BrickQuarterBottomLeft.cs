namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickQuarterBottomLeft(int gridX, int gridY) : TileBase(gridX, gridY, 2, 7, 1, 0, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
