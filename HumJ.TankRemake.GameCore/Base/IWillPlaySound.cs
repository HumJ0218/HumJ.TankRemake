namespace HumJ.TankRemake.GameCore.Base
{
    public interface  IWillPlaySound
    {
        event EventHandler<string>? OnPlaySound;
    }
}