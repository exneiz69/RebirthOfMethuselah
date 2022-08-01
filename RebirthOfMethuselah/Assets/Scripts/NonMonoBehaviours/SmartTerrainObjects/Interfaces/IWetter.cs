public interface IWetter
{
    public bool CanWet { get; }

    public void Wet(IWettable target);
}