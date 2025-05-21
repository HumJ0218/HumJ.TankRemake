using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponApGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Ap;
        public override int ClipAmmo => 10;

        private int lastFireTick = 0;

        public override BulletBase[] Fire(TankBase tank, Playground playground)
        {
            if (CurrentAmmo < 1)
            {
                return tank.PrimaryWeapon.Fire(tank, playground);
            }

            var fireInterval = tank.SpeedLevel switch
            {
                1 => 40,
                2 => 30,
                3 => 24,
                4 => 20,
                _ => throw new NotImplementedException(),
            };

            if (playground.Tick - lastFireTick < fireInterval)
            {
                return [];
            }

            var bullet = new BulletApGun(BulletStartPosition(tank), tank.Direction, tank.Direction switch
            {
                EntityDirection.Up => new(0, -10),
                EntityDirection.Right => new(10, 0),
                EntityDirection.Down => new(0, 10),
                EntityDirection.Left => new(-10, 0),
                _ => throw new NotImplementedException(),
            }, tank.Camp, tank.PowerLevel > 3 ? 9 : 8);

            CurrentAmmo--;

            lastFireTick = playground.Tick;
            PlaySound("S_Armor");
            return [bullet];
        }
    }
}
