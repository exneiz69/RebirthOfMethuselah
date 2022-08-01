using UnityEngine;
using UnityEngine.Events;

public class PickableBurnableSTObject : PickableSTObject, IBurnable
{
    [SerializeField] private UnityEvent _burned;

    public bool IsBurned { get; private set; } = false;

    public bool CanSmoke { get; private set; } = false;

    public void Burn()
    {
        IsBurned = true;
        CanSmoke = true;
        _burned?.Invoke();
    }

    public void Smoke(ISmokable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanSmoke)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Smoke();
        }
    }
}