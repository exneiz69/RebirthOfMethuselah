using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class STObjectActionsView : MonoBehaviour
{
    [SerializeField] private ActionSelectorView _prefab;
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private float _radius;

    private STObjectActionsParser _actionsParser;

    public event UnityAction CloseActionPerformed;
    public event UnityAction<IPickable> PickUpActionPerformed;

    #region MonoBehaviour

    private void Awake()
    {
        _actionsParser = new STObjectActionsParser();
    }

    private void OnValidate()
    {
        _radius = _radius < 0 ? 0 : _radius;
    }

    #endregion

    public void Render(STObject passive)
    {
        IReadOnlyDictionary<string, UnityAction> actions = _actionsParser.Parse(passive, OnCloseActionPerformed, OnPickUpActionPerformed);
        Render(actions);
    }

    public void Render(STObject active, STObject passive)
    {
        IReadOnlyDictionary<string, UnityAction> actions = _actionsParser.Parse(active, passive, OnCloseActionPerformed, OnPickUpActionPerformed);
        Render(actions);
    }

    public void Hide()
    {
        foreach (Transform child in _group.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Render(IReadOnlyDictionary<string, UnityAction> actions)
    {
        Hide();
        int actionsCount = actions.Count;
        int actionIndex = 0;
        foreach (KeyValuePair<string, UnityAction> action in actions)
        {
            float angle = (2f * Mathf.PI * actionIndex) / actionsCount;
            var position = new Vector3(Mathf.Sin(angle) * _radius, -1 * Mathf.Cos(angle) * _radius, 0);
            ActionSelectorView actionSelector = Instantiate(_prefab, _group.transform);
            actionSelector.transform.localPosition = position;
            actionSelector.Render(action.Key);
            actionSelector.Selected += action.Value;
            actionIndex++;
        }
    }

    private void OnCloseActionPerformed()
    {
        Hide();
        CloseActionPerformed?.Invoke();
    }

    private void OnPickUpActionPerformed(IPickable pickable)
    {
        PickUpActionPerformed?.Invoke(pickable);
    }
}
