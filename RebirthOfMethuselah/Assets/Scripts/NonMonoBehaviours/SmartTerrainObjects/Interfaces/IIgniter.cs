public interface IIgniter
{
    public bool CanIgnite { get; }

    public void Ignite(IBurnable target);
}