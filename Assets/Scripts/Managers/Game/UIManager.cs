using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private bool _isGameStarting = true;
    private bool _inputReady = false;

    [Header("UI Canvases")]
    [Space]

    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _permamentCanvas;

    [Header("Loading UI")]
    [Space]

    [SerializeField] private Slider _loadingSlider;

    [Header("HUD")]
    [Space]

    [SerializeField] private TextMeshProUGUI _attemptsText;

    [Header("Zone Settings")]
    [Space]

    [SerializeField] private RectTransform _checkZone;

    [Header("Other")]
    [Space]

    [SerializeField] private PlayerData _playerData;
    private void Start()
    {
        InitializeGameState();
        StartCoroutine(WaitForAllKeysToRelease());
    }

    private void Update()
    {
        if (_inputReady && Input.anyKeyDown && _isGameStarting && IsCursorInZone())
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

        UpdateHUD();

        SetCanvasState(_startCanvas, true);
        SetCanvasState(_loadingCanvas, false);
        SetCanvasState(_gameCanvas, false);
        SetCanvasState(_pauseCanvas, false);
        SetCanvasState(_permamentCanvas, true);
    }

    private IEnumerator WaitForAllKeysToRelease()
    {
        yield return null;

        float timeout = 5f;
        float elapsedTime = 0f;

        while (Input.anyKey && elapsedTime < timeout)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        _inputReady = true;
    }

    public void StartGame()
    {
        _isGameStarting = false;

        Time.timeScale = 1;
        SetCanvasState(_startCanvas, false);
        SetCanvasState(_loadingCanvas, false);
        SetCanvasState(_gameCanvas, true);
        SetCanvasState(_pauseCanvas, false);
        SetCanvasState(_permamentCanvas, true);

        Debug.Log("Игра началась!");
    }
    #endregion
    #region HUD
    private void UpdateHUD()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        _attemptsText.text = $"{_playerData.LevelsData[currentScene.name].Attempts.ToString()} попытка";
    }
    #endregion
    #region Zone Check
    private bool IsCursorInZone()
    {
        if (_checkZone == null) return false;

        Vector2 cursorPosition = Input.mousePosition;
        return RectTransformUtility.RectangleContainsScreenPoint(_checkZone, cursorPosition);
    }
    #endregion
    #region Pause Menu
    public void OpenPauseMenu()
    {
        if (Time.timeScale == 0) return;

        Time.timeScale = 0;
        SetCanvasState(_gameCanvas, false);
        SetCanvasState(_pauseCanvas, true);
    }

    public void ClosePauseMenu()
    {
        if (Time.timeScale == 1) return;

        Time.timeScale = 1;
        SetCanvasState(_pauseCanvas, false);
        SetCanvasState(_gameCanvas, true);
    }

    public void ReturnToMainMenu()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        _playerData.LevelsData[currentSceneName].CurrentCheckpoint = 0;
        LoadSceneByName("MainMenu");
    }
    #endregion
    #region Loading Panel
    public void StartLoading()
    {
        Time.timeScale = 0;

        SetCanvasState(_loadingCanvas, true);
        SetCanvasState(_startCanvas, false);
        SetCanvasState(_gameCanvas, false);
        SetCanvasState(_pauseCanvas, false);
    }
    #endregion
    #region Scene Management
    public void LoadSceneByName(string sceneName)
    {
        if (SceneExists(sceneName))
        {
            StartLoading();
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        else
        {
            Debug.LogError($"Scene \"{sceneName}\" does not exist in Build Settings.");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (_loadingSlider != null)
                _loadingSlider.value = progress;

            if (_loadingSlider != null)
                Debug.Log($"{progress * 100:0}%");

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName.Equals(sceneName, StringComparison.OrdinalIgnoreCase))
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
