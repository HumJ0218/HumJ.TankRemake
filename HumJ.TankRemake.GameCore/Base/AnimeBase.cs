namespace HumJ.TankRemake.GameCore.Base
{
    public abstract class AnimeBase(Texture[] textures) : IHaveTexture
    {
        public abstract Texture? this[int tick] { get; }

        protected Texture[] Textures => textures;
    }
}