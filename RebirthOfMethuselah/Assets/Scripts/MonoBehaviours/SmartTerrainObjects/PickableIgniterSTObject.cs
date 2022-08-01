using UnityEngine;

public class PickableIgniterSTObject : PickableSTObject, IIgniter
{
    public bool CanIgnite { private set; get; } = true;

    public void Ignite(IBurnable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanIgnite)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Burn();
        }
    }
}
