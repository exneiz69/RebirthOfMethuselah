using UnityEngine;

public class PausePanel : HideablePanel
{
    [SerializeField] private HUDPanel _hudPanel;

    public override void Show()
    {
        base.Show();
        _hudPanel.Hide();
    }

    public override void Hide()
    {
        base.Hide();
        _hudPanel.Show();
    }
}