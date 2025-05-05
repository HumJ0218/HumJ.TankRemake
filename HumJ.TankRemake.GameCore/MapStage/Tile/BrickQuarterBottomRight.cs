namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickQuarterBottomRight(int gridX, int gridY) : TileBase(gridX, gridY, 0)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
