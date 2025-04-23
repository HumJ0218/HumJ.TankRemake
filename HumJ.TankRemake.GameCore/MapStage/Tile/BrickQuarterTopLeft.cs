namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickQuarterTopLeft(int gridX, int gridY) : TileBase(gridX, gridY, 2, 4, 1, 0, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
