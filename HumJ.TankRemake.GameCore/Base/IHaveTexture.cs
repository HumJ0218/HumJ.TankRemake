namespace HumJ.TankRemake.GameCore.Base
{
    public interface IHaveTexture
    {
        Texture? this[int tick] { get; }
    }
}