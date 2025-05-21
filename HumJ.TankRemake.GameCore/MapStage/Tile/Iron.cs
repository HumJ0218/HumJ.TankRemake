using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class Iron(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;

        public override (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet) => (8, bullet is BulletApGun ? null : this);
    }
}