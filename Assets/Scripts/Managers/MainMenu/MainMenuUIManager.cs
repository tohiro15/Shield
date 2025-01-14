using TMPro;
using UnityEngine;
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

    [Header("ManualMenu")]
    [Space]

    [SerializeField] private TextMeshProUGUI _openMenuDescriptionText;
    [SerializeField] private GameObject _manualControl;
    [SerializeField] private GameObject _manualGameplay;

    [Header("Options")]
    [Space]

    [Header("Volume")]
    [SerializeField] private Slider _volumeSlider;
    public string volumeParameter = "MasterVolume";

    private void SetVolume(float value)
    {
        SoundManager.Instance.SetVolume(volumeParameter, value);
    }

    private void Start()
    {
        _mainMenuCanvas.SetActive(true);
        _optionsCanvas.SetActive(false);
        _manualCanvas.SetActive(false);
        _levelSelectionCanvas.SetActive(true);

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            if (i + 1 == _currentChapter)
            {
                _chapterLevels[i].SetActive(true);
            }
            else
            {
                _chapterLevels[i].SetActive(false);
            }
        }

        _currentChapterText.text = $"Глава - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(false);
        _nextChapterButton.gameObject.SetActive(true);

        float savedVolume = SoundManager.Instance.GetVolume(volumeParameter);
        _volumeSlider.value = savedVolume;

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    #region MainMenuButtons
    public void OpenToManualMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _optionsCanvas.SetActive(false);
        _manualCanvas.SetActive(true);
        _levelSelectionCanvas.SetActive(false);

        _openMenuDescriptionText.gameObject.SetActive(true);
        _manualControl.SetActive(false);
        _manualGameplay.SetActive(false);
    }
    public void OpenLevelSelection()
    {
        _mainMenuCanvas.SetActive(true);
        _optionsCanvas.SetActive(false);
        _manualCanvas.SetActive(false);
        _levelSelectionCanvas.SetActive(true);
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
        _mainMenuCanvas.SetActive(true);
        _optionsCanvas.SetActive(true);
        _manualCanvas.SetActive(false);
        _levelSelectionCanvas.SetActive(false);
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
        _currentChapterText.text = $"Глава - {_currentChapter}";

        _pastChapterButton.gameObject.SetActive(true);

        if (_currentChapter != _chapterLevels.Length)
        {
            _nextChapterButton.gameObject.SetActive(true);
        }
        else
        {
            _nextChapterButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            if (i + 1 == _currentChapter) _chapterLevels[i].SetActive(true);
            else _chapterLevels[i].SetActive(false);
        }
    }

    public void PastChapterChange()
    {
        _currentChapter--;
        _currentChapterText.text = $"Глава - {_currentChapter}";

        if(_currentChapter != _chapterLevels.Length) _nextChapterButton.gameObject.SetActive(true);

        if (_currentChapter > 1)
        {
            _pastChapterButton.gameObject.SetActive(true);
        }
        else
        {
            _pastChapterButton.gameObject.SetActive(false);
        }

        for (int i = 0; i < _chapterLevels.Length; i++)
        {
            if (i + 1 == _currentChapter) _chapterLevels[i].SetActive(true);
            else _chapterLevels[i].SetActive(false);
        }
    }
    #endregion
}
