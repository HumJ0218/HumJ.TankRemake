namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickHalfRight(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 1, 14, 2, variant, false)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;
    }
}
