using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("MainMenu Canvas")]
    [Space]

    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _manualCanvas;
    [SerializeField] private GameObject _levelSelectionCanvas;

    [Header("Change Chapter")]
    [Space]

    [SerializeField] private GameObject[] _chapterLevels;
    [SerializeField] private Button _nextChapterButton;
    [SerializeField] private Button _pastChapterButton;
    [SerializeField] private TextMeshProUGUI _currentChapterText;
    private int _currentChapter = 1;

    [Header ("Change Level")]
    [Space]

    [SerializeField] private TextMeshProUGUI[] _coinsCollected;

    [Header("Manual Menu")]
    [Space]

    [SerializeField] private TextMeshProUGUI _openMenuDescriptionText;
    [SerializeField] private GameObject _manualControl;
    [SerializeField] private GameObject _manualGameplay;

    [Header("Loading Menu")]
    [Space]

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Slider _loadingSlider;

    [Header("Options")]
    [Space]

    [SerializeField] private Slider _volumeSlider;

    [Header("Other")]
    [Space]

    [SerializeField] private PlayerData _playerData;
    
    public string volumeParameter = "MasterVolume";

    private void SetVolume(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }

    private void Start()
    {
        if (SoundManager.Instance == null)
        {
            Debug.LogError("SoundManager not found!");
            return;
        }

        SetCanvasState(true, false, false, true);

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            _chapterLevels[i].SetActive(i + 1 == _currentChapter);
        }

        _currentChapterText.text = $"Глава - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(false);
        _nextChapterButton.gameObject.SetActive(true);

        for (int i = 0; i < _coinsCollected.Length; i++)
        {
            _coinsCollected[i].text = $"{_playerData.LevelsData[i].CoinsCollected.ToString()} собрано";
        }

        float savedVolume = SoundManager.Instance.GetMusicVolume();
        _volumeSlider.value = savedVolume;

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetCanvasState(bool mainMenu, bool options, bool manual, bool levelSelection, bool loading = false)
    {
        _mainMenuCanvas.SetActive(mainMenu);
        _optionsCanvas.SetActive(options);
        _manualCanvas.SetActive(manual);
        _levelSelectionCanvas.SetActive(levelSelection);
        _loadingCanvas.SetActive(loading);
    }

    #region MainMenuButtons
    public void OpenToManualMenu()
    {
        SetCanvasState(true, false, true, false);
        _openMenuDescriptionText.gameObject.SetActive(true);
        _manualControl.SetActive(false);
        _manualGameplay.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        SetCanvasState(true, false, false, true);
    }

    public void OpenManualControl()
    {
        _openMenuDescriptionText.gameObject.SetActive(false);
        _manualControl.SetActive(true);
        _manualGameplay.SetActive(false);
    }

    public void OpenManualGameplay()
    {
        _openMenuDescriptionText.gameObject.SetActive(false);
        _manualControl.SetActive(false);
        _manualGameplay.SetActive(true);
    }

    public void OpenOptionsMenu()
    {
        SetCanvasState(true, true, false, false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Вы вышли из игры.");
    }
    #endregion
    #region Loading

    public void LoadLevel(string levelName)
    {
        string sceneName = GetScenePath(levelName);
        if (sceneName != null)
        {
            SetCanvasState(false, false, false, false, true );
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        else
        {
            Debug.LogError($"Уровень с именем \"{levelName}\" не найден в сборке!");
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
    }

    private string GetScenePath(string levelName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

            if (sceneName.Equals(levelName, System.StringComparison.OrdinalIgnoreCase))
            {
                return path;
            }
        }
        return null;
    }
    #endregion
    #region ChangeChapter
    public void NextChapterChange()
    {
        _currentChapter++;
        UpdateChapterUI();
    }

    public void PastChapterChange()
    {
        _currentChapter--;
        UpdateChapterUI();
    }

    private void UpdateChapterUI()
    {
        _currentChapterText.text = $"Глава - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(_currentChapter > 1);
        _nextChapterButton.gameObject.SetActive(_currentChapter < _chapterLevels.Length);

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            _chapterLevels[i].SetActive(i + 1 == _currentChapter);
        }
    }
    #endregion
}
