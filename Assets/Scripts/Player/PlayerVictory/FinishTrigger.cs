using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene == SceneManager.GetSceneByName("Development"))
        {
            SceneManager.LoadScene(currentScene.path);
        }
        else
        {
            int nextSceneIndex = currentScene.buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("Нет следующей сцены для загрузки!");
                SceneManager.LoadScene(0);
            }
        }
    }
}
