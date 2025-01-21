using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] private GameObject _loadingCanvas;
    //[SerializeField] private Slider _loadingSlider;

    //private void Start()
    //{
    //    _loadingCanvas.SetActive(false);
    //}
    //public void LoadLevel(string levelName)
    //{
    //    string sceneName = GetScenePath(levelName);
    //    if (sceneName != null)
    //    {
    //        _loadingCanvas.SetActive(true);
    //        StartCoroutine(LoadSceneAsync(sceneName));
    //    }
    //    else
    //    {
    //        Debug.LogError($"Уровень с именем \"{levelName}\" не найден в сборке!");
    //    }
    //}
    //private IEnumerator LoadSceneAsync(string sceneName)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //    operation.allowSceneActivation = false;

    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(operation.progress / 0.9f);

    //        if (_loadingSlider != null)
    //            _loadingSlider.value = progress;

    //        if (_loadingSlider != null)
    //            Debug.Log($"{progress * 100:0}%");

    //        if (operation.progress >= 0.9f)
    //        {
    //            operation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}

    //private string GetScenePath(string levelName)
    //{
    //    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    //    {
    //        string path = SceneUtility.GetScenePathByBuildIndex(i);
    //        string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

    //        if (sceneName.Equals(levelName, System.StringComparison.OrdinalIgnoreCase))
    //        {
    //            return path;
    //        }
    //    }
    //    return null;
    //}
}
