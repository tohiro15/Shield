using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("MainMenu Canvas")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _manualCanvas;
    [SerializeField] private GameObject _levelSelectionCanvas;

    [Header("Change Chapter")]
    [SerializeField] private GameObject[] _chapterLevels;
    [SerializeField] private Button _nextChapterButton;
    [SerializeField] private Button _pastChapterButton;
    [SerializeField] private TextMeshProUGUI _currentChapterText;
    private int _currentChapter = 1;

    [Header("ManualMenu")]
    [SerializeField] private TextMeshProUGUI _openMenuDescriptionText;
    [SerializeField] private GameObject _manualControl;
    [SerializeField] private GameObject _manualGameplay;

    [Header("Options")]
    [SerializeField] private Slider _volumeSlider;
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

        float savedVolume = SoundManager.Instance.GetMusicVolume();
        _volumeSlider.value = savedVolume;

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetCanvasState(bool mainMenu, bool options, bool manual, bool levelSelection)
    {
        _mainMenuCanvas.SetActive(mainMenu);
        _optionsCanvas.SetActive(options);
        _manualCanvas.SetActive(manual);
        _levelSelectionCanvas.SetActive(levelSelection);
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
