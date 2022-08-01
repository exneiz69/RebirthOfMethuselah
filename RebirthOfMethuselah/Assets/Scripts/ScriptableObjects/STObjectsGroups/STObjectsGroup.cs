using UnityEngine;

public abstract class STObjectsGroup : ScriptableObject
{
    public abstract STObject[] STObjects { get; }

    public int Count => STObjects.Length;

    public STObject this[int i] => STObjects[i];

}

public abstract class STObjectsGroup<T> : STObjectsGroup where T : class
{
    [SerializeField] private STObject[] _stObjects;

    public override STObject[] STObjects => _stObjects;

    #region ScriptableObject

    protected virtual void OnValidate()
    {
        for (int i = 0; i < _stObjects?.Length; i++)
        {
            if (_stObjects[i]?.GetComponent<T>() is null)
            {
                _stObjects[i] = null;
            }
        }
    }

    #endregion
}
