using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Ice(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Terrain;

        public override bool HitWith(TankBase tank) => false;

        public bool OnIce(TankBase tank) => tank.Enhance != EnhanceType.Ice && base.HitWith(tank);
    }
}
