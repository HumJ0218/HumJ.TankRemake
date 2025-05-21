using HumJ.TankRemake.GameCore.Tank;
using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Water(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Terrain;

        public override bool HitWith(TankBase tank) => OnWater(tank);

        public bool OnWater(TankBase tank) => tank.Enhance != EnhanceType.Water && base.HitWith(tank);
    }
}
