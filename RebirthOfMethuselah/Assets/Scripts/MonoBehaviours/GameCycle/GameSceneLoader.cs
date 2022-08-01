using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : MonoBehaviour
{
    [SerializeField] private int _gameSceneBuildIndex;

    public void Load()
    {
        SceneManager.LoadScene(_gameSceneBuildIndex);
    }
}
