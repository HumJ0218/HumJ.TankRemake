using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickHalfTop(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;

        public override (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet)
        {
            return bullet.Direction switch
            {
                Tank.EntityDirection.Up => (1, null),
                Tank.EntityDirection.Right => (1, new BrickQuarterTopRight(gridX, gridY)),
                Tank.EntityDirection.Down => (1, null),
                Tank.EntityDirection.Left => (1, new BrickQuarterTopLeft(gridX, gridY)),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
