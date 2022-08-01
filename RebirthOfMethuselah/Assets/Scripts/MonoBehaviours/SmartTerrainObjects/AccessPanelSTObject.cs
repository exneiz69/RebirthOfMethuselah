using UnityEngine;
using UnityEngine.Events;

public class AccessPanelSTObject : STObject, IAccessPanel
{
    [SerializeField] private UnityEvent _wetted;
    [SerializeField] private UnityEvent _signaled;

    public bool IsWetted { get; private set; } = false;

    public bool IsSignaled { get; private set; } = false;

    public void Wet()
    {
        if (!IsWetted)
        {
            IsWetted = true;
            _wetted?.Invoke();
        }
    }

    public void Signal()
    {
        IsSignaled = true;
        _signaled?.Invoke();
    }
}