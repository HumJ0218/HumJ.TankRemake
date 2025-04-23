using System.Drawing;

namespace HumJ.TankRemake.GameCore.Base
{
    public interface IHaveHitBox
    {
        RectangleF HitBox { get; }
    }
}