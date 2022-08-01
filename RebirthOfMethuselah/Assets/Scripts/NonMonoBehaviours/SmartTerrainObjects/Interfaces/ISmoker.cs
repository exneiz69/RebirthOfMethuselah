public interface ISmoker
{
    public bool CanSmoke { get; }

    public void Smoke(ISmokable target);
}