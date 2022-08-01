public interface IBurnable : ISmoker
{
    public bool IsBurned { get; }

    public void Burn();
}