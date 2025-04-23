namespace HumJ.TankRemake.GameCore.Base
{
    public class LoopAnime(Texture[] textures) : AnimeBase(textures)
    {
        public override Texture? this[int frame] => Textures[frame % Textures.Length];
    }
}