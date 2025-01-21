using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class FinishTrigger : MonoBehaviour
{
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("UIManager не найден в сцене!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _uiManager != null)
        {
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
                Debug.Log("Нет следующей сцены для загрузки!");
                _uiManager.LoadSceneByName("MainMenu");
            }
        }
    }
}
