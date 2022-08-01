using UnityEngine;

[CreateAssetMenu(fileName = "STObjectsInteractions", menuName = "STObjectsInteractions/Interactions", order = 151)]
public class STObjectsInteractions : ScriptableObject
{
    [SerializeField] private STObjectsInteraction[] _interactions;

    public int Count => _interactions.Length;

    public STObjectsInteraction this[int i] => _interactions[i];
}

[System.Serializable]
public class STObjectsInteraction
{
    [SerializeField] private STObjectsGroup _group;
    [SerializeField] private string _name;
    [SerializeField] private STObjectEffect _cause;
    [SerializeField] private STObjectEffect _effect;

    public STObjectsGroup Group => _group;

    public string Name => _name;

    public STObjectEffect Cause => _cause;

    public STObjectEffect Effect => _effect;
}