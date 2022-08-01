using UnityEngine;
using UnityEngine.Events;

public class GeneratorSTObject : STObject, IGenerator
{
    [SerializeField] private STObject _signalableTarget;
    [SerializeField] private UnityEvent _refueled;

    public bool IsRefueled { get; private set; } = false;

    public bool CanSignal { get; private set; } = false;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_signalableTarget?.GetComponent<ISignalable>() is null)
        {
            _signalableTarget = null;
        }
    }

    #endregion

    public void Refuel()
    {
        if (!IsRefueled)
        {
            IsRefueled = true;
            _refueled?.Invoke();
            CanSignal = true;
            Signal(_signalableTarget as ISignalable);
        }
    }

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
