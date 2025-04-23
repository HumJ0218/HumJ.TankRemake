namespace HumJ.TankRemake.GameCore.Base
{
    public interface IHaveControl<TControl> where TControl : Enum
    {
        bool this[TControl control] { get; set; }
    }
}