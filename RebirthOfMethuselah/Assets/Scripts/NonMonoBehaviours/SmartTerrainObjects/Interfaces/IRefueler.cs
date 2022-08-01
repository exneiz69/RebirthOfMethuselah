public interface IRefueler
{
    public bool CanRefuel { get; }

    public void Refuel(IRefuelable target);
}