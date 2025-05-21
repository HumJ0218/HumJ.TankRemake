using HumJ.TankRemake.GameCore.Weapon;

namespace HumJ.TankRemake.GameCore.MapStage.Tile
{
    public class BrickFull(int gridX, int gridY, int variant) : TileBase(gridX, gridY, variant)
    {
        public override TileLayer Layer { get; } = TileLayer.Building;

        public override (int Damaged, TileBase? ChangeTo) DamageBy(BulletBase bullet)
        {
            if (bullet.Damage >= 2)
            {
                return (2, null);
            }
            else
            {
                return bullet.Direction switch
                {
                    Tank.EntityDirection.Up => (1, new BrickHalfTop(gridX, gridY, variant % 2)),
                    Tank.EntityDirection.Right => (1, new BrickHalfRight(gridX, gridY, variant % 2)),
                    Tank.EntityDirection.Down => (1, new BrickHalfBottom(gridX, gridY, variant % 2)),
                    Tank.EntityDirection.Left => (1, new BrickHalfLeft(gridX, gridY, variant % 2)),
                    _ => throw new NotImplementedException(),
                };
            }
        }
    }
}