using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image[] _coinImages;
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
    }
    public void UpdateCoinStatus(int coinsCollected, string levelName)
    {
        for (int i = 0; i < _coinImages.Length; i++)
        {
            _coinImages[i].color = i < coinsCollected ? Color.yellow : Color.gray;
        }
    }


}
