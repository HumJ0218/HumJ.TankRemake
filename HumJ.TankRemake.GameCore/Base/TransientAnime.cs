namespace HumJ.TankRemake.GameCore.Base
{
    public class TransientAnime(Texture[] textures) : AnimeBase(textures)
    {
        public override Texture? this[int frame] => frame < Textures.Length ? Textures[frame] : null;
    }
}