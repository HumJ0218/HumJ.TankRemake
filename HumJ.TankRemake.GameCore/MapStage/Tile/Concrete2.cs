namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete2(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 3, 0, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
