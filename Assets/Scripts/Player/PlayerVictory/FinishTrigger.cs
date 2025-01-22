using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class FinishTrigger : MonoBehaviour
{
    private UIManager _uiManager;
    private Scene _currentScene;

    [SerializeField] private PlayerData _playerData;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _currentScene = SceneManager.GetActiveScene();
        if (_uiManager == null)
        {
            Debug.LogError("UIManager не найден в сцене!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _uiManager != null)
        {
            int levelIndex = _playerData.GetLevelIndexByName(_currentScene.name);

            if (levelIndex != -1)
            {
                _playerData.LevelsData[_currentScene.name].Attempts = 0;

                LoadNextScene();
            }
            else
            {
                RestartCurrentScene();
            }
        }
    }


    private void LoadNextScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        int nextSceneIndex = currentScene.buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            nextSceneName = Path.GetFileNameWithoutExtension(nextSceneName);

            _uiManager.LoadSceneByName(nextSceneName);
        }
        else
        {
            Debug.Log("Нет следующей сцены для загрузки!");
            _uiManager.LoadSceneByName("MainMenu");
        }
    }

    private void RestartCurrentScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        _uiManager.LoadSceneByName(currentScene.name);

        Debug.Log("Эта сцена должна быть бесконечной!");
    }
}
