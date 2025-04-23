namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete1(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 3, 4, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
