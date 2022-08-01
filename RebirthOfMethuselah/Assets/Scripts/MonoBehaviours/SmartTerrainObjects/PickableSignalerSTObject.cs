using UnityEngine;

public class PickableSignalerSTObject : PickableSTObject, ISignaler
{
    public bool CanSignal { get; private set; } = true;

    public void Signal(ISignalable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanSignal)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Signal();
        }
    }
}