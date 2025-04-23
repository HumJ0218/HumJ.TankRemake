namespace HumJ.TankRemake.GameCore.Weapon
{
    public abstract class WeaponBase
    {
        public abstract WeaponType Type { get; }
        public virtual int MaxAmmo => (int)Type * 5;
        public virtual int AmmoCount { get; private set; }
    }
}