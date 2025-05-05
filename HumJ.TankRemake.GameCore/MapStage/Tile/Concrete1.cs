namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete1(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
