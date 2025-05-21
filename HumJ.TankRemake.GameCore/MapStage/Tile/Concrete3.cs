using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Concrete3(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;

        public override (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet) => bullet.Damage switch
        {
            0 => (0, this),
            1 => (1, new Concrete2(gridX, gridY, variant)),
            2 => (2, new Concrete1(gridX, gridY, variant)),
            _ => (3, null),
        };
    }
}
