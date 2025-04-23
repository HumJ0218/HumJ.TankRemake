namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete3(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 2, 12, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
