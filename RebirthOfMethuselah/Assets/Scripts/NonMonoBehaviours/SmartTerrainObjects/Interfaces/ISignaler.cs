public interface ISignaler
{
    public bool CanSignal { get; }

    public void Signal(ISignalable target);
}