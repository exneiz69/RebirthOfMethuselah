using UnityEngine;

public abstract class STObject : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private bool _isPersistent;

    public string Name => _name;

    public bool IsPersistent => _isPersistent;
}