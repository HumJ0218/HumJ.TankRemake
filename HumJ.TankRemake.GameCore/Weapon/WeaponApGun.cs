using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponApGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Ap;
        public override int ClipAmmo => 10;

        public override BulletApGun[] Fire(TankBase tank, Playground playground)
        {
            throw new NotImplementedException();
        }
    }
}
