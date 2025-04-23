namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Crystal(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 3, 12, 4, variant, true)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
