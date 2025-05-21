using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete2(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;

        public override (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet) => bullet.Damage switch
        {
            0 => (0, this),
            1 => (1, new Concrete1(gridX, gridY, variant)),
            _ => (2, null),
        };
    }
}
