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

    [Header("Change Level")]
    [Space]

    [SerializeField] private LevelButton[] _levelButtons;

    [Header("Manual Menu")]
    [Space]

    [SerializeField] private TextMeshProUGUI _openMenuDescriptionText;
    [SerializeField] private GameObject _manualControl;
    [SerializeField] private GameObject _manualGameplay;

    [Header("Loading Menu")]
    [Space]

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Slider _loadingSlider;
    private bool isLoading = false;

    [Header("Options")]
    [Space]

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Other")]
    [Space]
    [SerializeField] private PlayerData _playerData;
    
    public string volumeParameter = "MasterVolume";

    private void SetMusicVolume(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }
    private void SetSFXVolume(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }

    private void Start()
    {
        Time.timeScale = 1;

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

        _currentChapterText.text = $"����� - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(false);
        _nextChapterButton.gameObject.SetActive(true);

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            string levelName = $"Level_{i + 1}";

            if (_playerData.LevelsData.ContainsKey(levelName))
            {
                int coinsCollected = _playerData.LevelsData[levelName].CoinsCollected;

                _levelButtons[i].UpdateCoinStatus(coinsCollected, levelName);
            }
            else
            {
                Debug.LogWarning($"���� ��� ������ '{levelName}' �� ������!");
                _levelButtons[i].UpdateCoinStatus(0, levelName);
            }

            int levelIndex = i; 
            _levelButtons[i].GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelName));
        }

        float savedMusicVolume = SoundManager.Instance.GetMusicVolume();
        _musicSlider.value = savedMusicVolume;

        float savedSFXVolume = SoundManager.Instance.GetSFXVolume();
        _sfxSlider.value = savedSFXVolume;

        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
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
        Debug.Log("�� ����� �� ����.");
    }
    #endregion
    #region Loading

    public void LoadLevel(string levelName)
    {
        if (isLoading) return; // ���� ��� ��� ��������, ������ �������.

        isLoading = true;

        string sceneName = GetScenePath(levelName);
        if (sceneName != null)
        {
            SetCanvasState(false, false, false, false, true);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
        else
        {
            Debug.LogError($"������� � ������ \"{levelName}\" �� ������ � ������!");
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
            {
                _loadingSlider.value = progress;
            }

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        isLoading = false; // �������� ���������
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
        _currentChapterText.text = $"����� - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(_currentChapter > 1);
        _nextChapterButton.gameObject.SetActive(_currentChapter < _chapterLevels.Length);

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            _chapterLevels[i].SetActive(i + 1 == _currentChapter);
        }
    }
    #endregion
}
