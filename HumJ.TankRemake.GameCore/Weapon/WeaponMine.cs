using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class WeaponMine : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Mine;
        public override int ClipAmmo => 15;

        public override BulletMine[] Fire(TankBase tank, Playground playground)
        {
            throw new NotImplementedException();
        }
    }
}
