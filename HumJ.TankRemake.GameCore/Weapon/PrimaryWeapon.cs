namespace HumJ.TankRemake.GameCore.Weapon
{
    public class PrimaryWeapon : WeaponBase
    {
        public override WeaponType Type { get; } = WeaponType.Primary;
        public override int MaxAmmo => int.MaxValue;
        public override int AmmoCount => int.MaxValue;
    }
}
