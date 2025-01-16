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
            Debug.LogError($"������� � ������ \"{levelName}\" �� ������!");
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
            Debug.LogError($"������� ��� ���������� � ������ \"{levelName}\" �� ������!");
        }
    }

    private bool IsLevelExists(string levelName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
        return sceneIndex != -1;
    }
}
