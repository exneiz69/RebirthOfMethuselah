using TMPro;
using UnityEngine;

public class STObjectView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Render(STObject stObject)
    {
        _text.text = stObject.Name;
    }

    public void Clear()
    {
        _text.text = string.Empty;
    }
}
