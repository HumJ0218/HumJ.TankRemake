using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponNormalGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Normal;
        public override int ClipAmmo => 0;
        public override int MaxAmmo => 0;

        private int lastFireTick = 0;

        public override BulletBase[] Fire(TankBase tank, Playground playground)
        {
            var fireInterval = tank.SpeedLevel switch
            {
                1 => 12,
                2 => 10,
                3 => 8,
                4 => 6,
                _ => throw new NotImplementedException(),
            };

            if (playground.Tick - lastFireTick < fireInterval)
            {
                return [];
            }

            var offset = (float)(tank.PowerLevel switch
            {
                1 => Random.Shared.NextDouble() * 0 - 0,
                2 => Random.Shared.NextDouble() * 6 - 3,
                3 => Random.Shared.NextDouble() * 12 - 6,
                4 => Random.Shared.NextDouble() * 18 - 9,
                _ => throw new NotImplementedException(),
            });

            var bullet = new BulletNormalGun(BulletStartPosition(tank) + tank.Direction switch
            {
                EntityDirection.Up => new(offset, 0),
                EntityDirection.Right => new(0, offset),
                EntityDirection.Down => new(offset, 0),
                EntityDirection.Left => new(0, offset),
                _ => throw new NotImplementedException(),
            }, tank.Direction, tank.Direction switch
            {
                EntityDirection.Up => new(0, -8),
                EntityDirection.Right => new(8, 0),
                EntityDirection.Down => new(0, 8),
                EntityDirection.Left => new(-8, 0),
                _ => throw new NotImplementedException(),
            }, tank.Camp);

            lastFireTick = playground.Tick;
            PlaySound("S_Normal");
            return [bullet];
        }
    }
}