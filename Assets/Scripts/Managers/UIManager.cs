using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("MainMenu UI")]
    [Space]

    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _manualCanvas;
    [SerializeField] private GameObject _levelSelectionCanvas;
    [Header("Change Chapter")]
    [SerializeField] private GameObject[] _chapterLevels;

    [SerializeField] private Button _nextChapterButton;
    [SerializeField] private Button _pastChapterButton;
    [SerializeField] private TextMeshProUGUI _currentChapterText;
    private int _currentChapter = 1;

    private void Start()
    {
        _mainMenuCanvas.SetActive(true);
        _manualCanvas.SetActive(true);
        _levelSelectionCanvas.SetActive(false);

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
    }

    #region MainMenuButtons
    public void OpenLevelSelection()
    {
        _manualCanvas.SetActive(false);
        _levelSelectionCanvas.SetActive(true);
    }

    public void ReturnToManualMenu()
    {
        _manualCanvas.SetActive(true);
        _levelSelectionCanvas.SetActive(false);
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
