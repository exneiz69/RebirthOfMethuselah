using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ActionSelectorView : MonoBehaviour
{
    private Button _actionSelector;
    private TMP_Text _tmpText;

    public event UnityAction Selected;

    #region MonoBehaviour

    private void Awake()
    {
        _actionSelector = GetComponent<Button>();
        _tmpText = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _actionSelector.onClick.AddListener(OnActionSelectorClicked);
    }

    private void OnDisable()
    {
        _actionSelector.onClick.RemoveListener(OnActionSelectorClicked);
    }

    #endregion

    public void Render(string text)
    {
        _tmpText.text = text;
    }

    public void OnActionSelectorClicked()
    {
        Selected?.Invoke();
    }
}