using UnityEngine;
using UnityEngine.UI;

public class PausePanelButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _resume;
    [SerializeField] private Button _exit;
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private GameCycle _gameCycle;

    #region MonoBehaviour

    private void OnEnable()
    {
        _resume.onClick.AddListener(OnResumeButtonClicked);
        _exit.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _resume.onClick.RemoveListener(OnResumeButtonClicked);
        _exit.onClick.RemoveListener(OnExitButtonClicked);
    }

    #endregion

    private void OnResumeButtonClicked()
    {
        _pausePanel.Hide();
    }

    private void OnExitButtonClicked()
    {
        _gameCycle.Quit();
    }
}