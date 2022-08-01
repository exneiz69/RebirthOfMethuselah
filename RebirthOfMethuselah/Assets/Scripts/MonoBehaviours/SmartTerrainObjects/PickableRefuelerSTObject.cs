using UnityEngine;

public class PickableRefuelerSTObject : PickableSTObject, IRefueler
{
    public bool CanRefuel { get; private set; } = true;

    public void Refuel(IRefuelable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanRefuel)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Refuel();
        }
    }
}
