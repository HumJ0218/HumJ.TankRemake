using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponFireGun : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Fire;
        public override int ClipAmmo => 30;

        public override BulletFireGun[] Fire(TankBase tank, Playground playground)
        {
            throw new NotImplementedException();
        }
    }
}
