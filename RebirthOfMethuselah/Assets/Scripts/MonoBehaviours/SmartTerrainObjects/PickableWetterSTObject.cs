using UnityEngine;

public class PickableWetterSTObject : PickableSTObject, IWetter
{
    public bool CanWet { get; private set; } = true;

    public void Wet(IWettable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanWet)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Wet();
        }
    }
}
