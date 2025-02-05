using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Canvases")]
    [SerializeField] private CanvasManager _canvasManager;

    [Header("Loading UI")]
    [SerializeField] private Slider _loadingSlider;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI _attemptsText;

    [Header("Zone Settings")]
    [SerializeField] private RectTransform _checkZone;

    [Header("Other")]
    [SerializeField] private PlayerData _playerData;

    private bool isGameStarting = true;
    private bool inputReady = false;
    private bool isSceneLoading = false;

    private void Start()
    {
        InitializeGameState();
        StartCoroutine(WaitForAllKeysToRelease());
    }

    private void Update()
    {
        if (inputReady && isGameStarting && Input.anyKeyDown && IsCursorInZone())
        {
            StartGame();
        }
    }

    #region Game State Management

    private void InitializeGameState()
    {
        isGameStarting = true;
        inputReady = false;

        UpdateHUD();
        _canvasManager.SetState(CanvasState.Start);

        Time.timeScale = 0;
    }

    private IEnumerator WaitForAllKeysToRelease()
    {
        float timeout = 5f;
        float elapsedTime = 0f;

        while (Input.anyKey && elapsedTime < timeout)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        inputReady = true;
    }

    public void StartGame()
    {
        isGameStarting = false;
        Time.timeScale = 1;
        _canvasManager.SetState(CanvasState.Game);

        Debug.Log("Game started!");
    }

    #endregion

    #region HUD

    private void UpdateHUD()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        _attemptsText.text = $"{_playerData.LevelsData[currentSceneName].Attempts} попытка";
    }

    #endregion

    #region Zone Check

    private bool IsCursorInZone()
    {
        return _checkZone != null && RectTransformUtility.RectangleContainsScreenPoint(_checkZone, Input.mousePosition);
    }

    #endregion

    #region Pause Menu

    public void OpenPauseMenu()
    {
        if (Time.timeScale == 0) return;

        Time.timeScale = 0;
        _canvasManager.SetState(CanvasState.Pause);
    }

    public void ClosePauseMenu()
    {
        if (Time.timeScale == 1) return;

        Time.timeScale = 1;
        _canvasManager.SetState(CanvasState.Game);
    }

    public void ReturnToMainMenu()
    {
        if (!isSceneLoading)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            _playerData.LevelsData[currentSceneName].CurrentCheckpoint = 0;

            if (currentSceneName != "MainMenu")
            {
                LoadSceneByName("MainMenu");
            }
        }
    }

    #endregion

    #region Loading Panel

    public void StartLoading()
    {
        Time.timeScale = 0;
        isGameStarting = false;
        _canvasManager.SetState(CanvasState.Loading);
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
        Debug.Log($"Начата загрузка сцены: {sceneName}");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timeout = 15f; // Максимальное время ожидания загрузки
        float elapsedTime = 0f;

        while (!operation.isDone && elapsedTime < timeout)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log($"Прогресс загрузки сцены {sceneName}: {progress * 100}%");

            if (_loadingSlider != null)
            {
                _loadingSlider.value = progress;
            }

            if (operation.progress >= 0.9f)
            {
                Debug.Log("Сцена готова к активации. Активируем сцену...");
                operation.allowSceneActivation = true;
            }

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        if (elapsedTime >= timeout)
        {
            Debug.LogError("Превышено время ожидания загрузки сцены!");
            // Здесь можно показать сообщение об ошибке пользователю или перезапустить загрузку
        }

        Debug.Log($"operation.progress: {operation.progress}, isDone: {operation.isDone}, allowSceneActivation: {operation.allowSceneActivation}");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (scene.name == "MainMenu")
        {
            Debug.Log("Main Menu scene loaded and activated.");
        }

        isSceneLoading = false;
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = Path.GetFileNameWithoutExtension(scenePath);
            Debug.Log($"Проверка сцены: {sceneFileName}");
            if (sceneFileName.Equals(sceneName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"Сцена {sceneName} найдена.");
                return true;
            }
        }
        Debug.LogError($"Сцена {sceneName} не найдена!");
        return false;
    }


    #endregion
}
