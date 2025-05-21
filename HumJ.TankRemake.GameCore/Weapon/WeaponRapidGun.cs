using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponRapidGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Rapid;
        public override int ClipAmmo => 50;

        private int lastFireTick = 0;

        public override BulletBase[] Fire(TankBase tank, Playground playground)
        {
            if (CurrentAmmo < 1)
            {
                return tank.PrimaryWeapon.Fire(tank, playground);
            }

            var fireInterval = tank.SpeedLevel switch
            {
                1 => 10,
                2 => 8,
                3 => 6,
                4 => 4,
                _ => throw new NotImplementedException(),
            };

            if (playground.Tick - lastFireTick < fireInterval)
            {
                return [];
            }

            var offset = (float)(tank.PowerLevel switch
            {
                1 => Random.Shared.NextDouble() * 0 - 0,
                2 => Random.Shared.NextDouble() * 5 - 0.25,
                3 => Random.Shared.NextDouble() * 10 - 5,
                4 => Random.Shared.NextDouble() * 15 - 7.5,
                _ => throw new NotImplementedException(),
            });

            var bullet = new BulletRapidGun(BulletStartPosition(tank) + tank.Direction switch
            {
                EntityDirection.Up => new(offset, 0),
                EntityDirection.Right => new(0, offset),
                EntityDirection.Down => new(offset, 0),
                EntityDirection.Left => new(0, offset),
                _ => throw new NotImplementedException(),
            }, tank.Direction, tank.Direction switch
            {
                EntityDirection.Up => new(offset/10, -10),
                EntityDirection.Right => new(10, offset / 10),
                EntityDirection.Down => new(offset / 10, 10),
                EntityDirection.Left => new(-10, offset / 10),
                _ => throw new NotImplementedException(),
            }, tank.Camp);

            var recycleBullet = tank.PowerLevel switch
            {
                1 => Random.Shared.NextDouble() < 0.0,
                2 => Random.Shared.NextDouble() < 0.16,
                3 => Random.Shared.NextDouble() < 0.32,
                4 => Random.Shared.NextDouble() < 0.48,
                _ => throw new NotImplementedException(),
            };

            if (!recycleBullet)
            {
                CurrentAmmo--;
            }

            lastFireTick = playground.Tick;
            PlaySound("S_Rapid");
            return [bullet];
        }
    }
}
