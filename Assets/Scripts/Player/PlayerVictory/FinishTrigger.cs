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
            Debug.LogError("UIManager �� ������ � �����!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _uiManager != null)
        {
            _playerData.LevelsData[_playerData.GetLevelIndexByName(_currentScene.name)].FailedAttempts = 0;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Development")
        {
            _uiManager.LoadSceneByName("Development");
        }
        else
        {


            int nextSceneIndex = currentScene.buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
                nextSceneName = Path.GetFileNameWithoutExtension(nextSceneName);

                _uiManager.LoadSceneByName(nextSceneName);
            }
            else
            {
                Debug.Log("��� ��������� ����� ��� ��������!");
                _uiManager.LoadSceneByName("MainMenu");
            }
        }
    }
}
