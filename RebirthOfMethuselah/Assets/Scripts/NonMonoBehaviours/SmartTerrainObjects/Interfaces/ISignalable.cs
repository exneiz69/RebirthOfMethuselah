public interface ISignalable
{
    public bool IsSignaled { get; }

    public void Signal();
}
