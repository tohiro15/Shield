using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _permanentCanvas;
    [SerializeField] private GameObject _pauseCanvas;

    public void SetState(CanvasState state)
    {
        _startCanvas.SetActive(state == CanvasState.Start);
        _loadingCanvas.SetActive(state == CanvasState.Loading);
        _gameCanvas.SetActive(state == CanvasState.Game);
        _permanentCanvas.SetActive(true);
        _pauseCanvas.SetActive(state == CanvasState.Pause);
    }
}

public enum CanvasState
{
    Start,
    Loading,
    Game,
    Permanent,
    Pause
}