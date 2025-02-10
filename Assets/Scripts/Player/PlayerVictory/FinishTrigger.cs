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
        InitializeDependencies();
    }

    private void InitializeDependencies()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _currentScene = SceneManager.GetActiveScene();

        if (_uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }

        if (_playerData == null)
        {
            Debug.LogError("PlayerData is not assigned in the inspector!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerTrigger();
        }
    }

    private void HandlePlayerTrigger()
    {
        if (_uiManager == null || _playerData == null)
        {
            Debug.LogError("Dependencies are not properly initialized!");
            return;
        }

        int levelIndex = _playerData.GetLevelIndexByName(_currentScene.name);

        if (levelIndex == 0)
        {
            ResetLevelProgress();
            RestartCurrentScene();
        }
        else if (levelIndex != -1)
        {
            CompleteLevelProgress();
            LoadNextScene();
        }
    }

    private void ResetLevelProgress()
    {
        if (!_playerData.LevelsData.ContainsKey(_currentScene.name))
        {
            Debug.LogError("Level data not found for the current scene!");
            return;
        }

        var levelData = _playerData.LevelsData[_currentScene.name];
        levelData.CurrentCheckpoint = 0;
    }

    private void CompleteLevelProgress()
    {
        if (!_playerData.LevelsData.ContainsKey(_currentScene.name))
        {
            Debug.LogError("Level data not found for the current scene!");
            return;
        }

        var levelData = _playerData.LevelsData[_currentScene.name];
        levelData.Attempts = 0;
        levelData.CurrentCoinsCollected = 0;
        levelData.CurrentCheckpoint = 0;
        levelData.IsDone = true;
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = _currentScene.buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextSceneName = GetSceneNameByIndex(nextSceneIndex);
            _uiManager.LoadSceneByName(nextSceneName);
        }
        else
        {
            Debug.Log("No more scenes to load! Returning to Main Menu.");
            _uiManager.LoadSceneByName("MainMenu");
        }
    }

    private string GetSceneNameByIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        return Path.GetFileNameWithoutExtension(scenePath);
    }

    private void RestartCurrentScene()
    {
        _uiManager.LoadSceneByName(_currentScene.name);
        Debug.Log("Restarting the current scene (endless mode).!");
    }
}
