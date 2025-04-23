using HumJ.TankRemake.GameCore.Weapon;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Tank
{
    public class PlayerTank : TankBase
    {
        public override TankCamp Camp => TankCamp.Player;

        public override bool Injured => false;
        public override TankType Type => TankType.Player1e;
        public override WeaponBase SecondaryWeapon => secondaryWeapon;

        public override int MaxHealthCount { get; } = 8;
        public override int MaxAmmoCount => SecondaryWeapon.MaxAmmo;
        public override int MaxSpeedLevel { get; } = 4;
        public override int MaxPowerLevel { get; } = 4;

        public override int HealthCount { get; } = 4;
        public override int AmmoCount => SecondaryWeapon.AmmoCount;
        public override int SpeedLevel { get; } = 4;
        public override int PowerLevel { get; } = 4;
        public override EnhanceType Enhance { get; } = EnhanceType.Memory;

        private WeaponBase secondaryWeapon;

        public PlayerTank(ILogger<PlayerTank> logger) : base(logger)
        {
            Position = new Vector2(17 * GridSize, 29 * GridSize);
            Direction = TankDirection.Up;

            secondaryWeapon = new SecondaryWeaponRapid();
        }
    }
}
