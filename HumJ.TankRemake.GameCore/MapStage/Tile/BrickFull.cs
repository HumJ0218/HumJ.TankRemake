namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickFull(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 1, 8, 4, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
