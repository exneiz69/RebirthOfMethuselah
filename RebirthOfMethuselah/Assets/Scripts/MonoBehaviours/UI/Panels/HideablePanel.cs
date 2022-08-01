using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public abstract class HideablePanel : MonoBehaviour
{
    [SerializeField] private bool _hideOnStart = true;

    private CanvasGroup _panel;

    public event UnityAction Shown;
    public event UnityAction Hidden;

    #region MonoBehaviour

    protected virtual void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        if (_hideOnStart)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    #endregion

    public virtual void Show()
    {
        _panel.alpha = 1;
        _panel.blocksRaycasts = true;
        Shown?.Invoke();
    }

    public virtual void Hide()
    {
        _panel.alpha = 0;
        _panel.blocksRaycasts = false;
        Hidden?.Invoke();
    }
}