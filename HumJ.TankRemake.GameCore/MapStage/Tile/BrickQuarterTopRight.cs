namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickQuarterTopRight(int gridX, int gridY) : TileBase(gridX, gridY, 2, 5, 1, 0, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
