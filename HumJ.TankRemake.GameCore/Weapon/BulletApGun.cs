﻿using HumJ.TankRemake.GameCore.Base;
using HumJ.TankRemake.GameCore.Tank;
using System.Drawing;
using System.Numerics;

namespace HumJ.TankRemake.GameCore.Weapon
{
    public class BulletApGun(Vector2 position, EntityDirection direction, Vector2 speed, Camp camp,int damage) : BulletBase(position, direction, speed, 60, camp, damage)
    {
        public override WeaponType Type => WeaponType.Ap;

        protected override IReadOnlyDictionary<EntityDirection, RectangleF> HitBoxByDirection { get; } = new Dictionary<EntityDirection, RectangleF>
        {
            [EntityDirection.Up] = new RectangleF(-4, -8, 8, 16),
            [EntityDirection.Right] = new RectangleF(-8, -4, 16, 8),
            [EntityDirection.Down] = new RectangleF(-4, -8, 8, 16),
            [EntityDirection.Left] = new RectangleF(-8, -4, 16, 8),
        };
    }
}