namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Iron(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 3, 8, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
