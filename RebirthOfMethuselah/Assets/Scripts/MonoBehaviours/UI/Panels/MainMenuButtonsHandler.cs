using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameSceneLoader _gameSceneLoader;
    [SerializeField] private GameCycle _gameCycle;

    #region MonoBehaviour

    void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    #endregion

    void OnPlayButtonClicked()
    {
        _gameSceneLoader.Load();
    }

    void OnExitButtonClicked()
    {
        _gameCycle.Quit();
    }
}
