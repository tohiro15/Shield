using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        if (IsLevelExists(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError($"Уровень с именем \"{levelName}\" не найден!");
        }
    }

    public void LoadDevelopment(string levelName)
    {
        if (IsLevelExists(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError($"Уровень для разработки с именем \"{levelName}\" не найден!");
        }
    }

    private bool IsLevelExists(string levelName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
        return sceneIndex != -1;
    }
}
