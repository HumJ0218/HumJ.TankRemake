using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Water(int gridX, int gridY, int variant) : TileBase(gridX, gridY, 0, 4, 4, variant, true)
    {
        public override TileLayer Layer { get; } = TileLayer.Terrain;

        public override bool HitWith(TankBase tank) => OnWater(tank);

        public bool OnWater(TankBase tank) => tank.Enhance != EnhanceType.Water && base.HitWith(tank);
    }
}
