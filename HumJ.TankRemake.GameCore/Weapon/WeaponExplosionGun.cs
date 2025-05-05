using HumJ.TankRemake.GameCore.Tank;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponExplosionGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Explosion;
        public override int ClipAmmo => 20;

        public override BulletExplosionGun[] Fire(TankBase tank, Playground playground)
        {
            throw new NotImplementedException();
        }
    }
}
