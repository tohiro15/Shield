using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private bool _isGameStarting = true;
    private bool _inputReady = false;

    [Header("UI Canvases")]
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _HUDCanvas;
    [SerializeField] private GameObject _pauseCanvas;

    private void Start()
    {
        InitializeGameState();
        StartCoroutine(WaitForAllKeysToRelease());
    }

    private void Update()
    {
        if (_inputReady && Input.anyKeyDown && _isGameStarting)
        {
            StartGame();
        }
    }

    #region Game State Management
    private void InitializeGameState()
    {
        Time.timeScale = 0;
        _isGameStarting = true;
        _inputReady = false;

        SetCanvasState(_startCanvas, true);
        SetCanvasState(_HUDCanvas, false);
        SetCanvasState(_pauseCanvas, false);
    }

    private IEnumerator WaitForAllKeysToRelease()
    {
        yield return null;

        while (Input.anyKey)
        {
            yield return null;
        }

        _inputReady = true;
    }

    public void StartGame()
    {
        _isGameStarting = false;

        Time.timeScale = 1;
        SetCanvasState(_startCanvas, false);
        SetCanvasState(_HUDCanvas, true);
        SetCanvasState(_pauseCanvas, false);

        Debug.Log("Игра началась!");
    }
    #endregion

    #region Pause Menu
    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        SetCanvasState(_HUDCanvas, false);
        SetCanvasState(_pauseCanvas, true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        SetCanvasState(_HUDCanvas, true);
        SetCanvasState(_pauseCanvas, false);
    }

    public void ReturnToMainMenu()
    {
        LoadSceneByName("MainMenu");
    }
    #endregion

    #region Scene Management
    private void LoadSceneByName(string sceneName)
    {
        if (SceneExists(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Scene \"{sceneName}\" does not exist in Build Settings.");
        }
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Utility
    private void SetCanvasState(GameObject canvas, bool isActive)
    {
        if (canvas != null)
        {
            canvas.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("Attempted to set state of a null canvas.");
        }
    }
    #endregion
}
