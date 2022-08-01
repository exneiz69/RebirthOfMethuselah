using UnityEngine;
using UnityEngine.Events;

public class ProjectorSTObject : STObject, IProjector
{
    [SerializeField] private UnityEvent _damaged;

    public bool IsDamaged { get; private set; } = false;

    public void Damage()
    {
        IsDamaged = true;
        _damaged?.Invoke();
    }
}