using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] private string[] _validTags;
    [SerializeField] private PlayerData _playerData;
    
    private SoundManager _soundManager;
    private Scene _currentScene;

    private void Awake()
    {
        _currentScene = SceneManager.GetActiveScene();
        _soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in _validTags)
        {
            if (other.CompareTag(tag))
            {
                _playerData.CoinPickUp(_currentScene.name);
                _soundManager.CoinPickupSoundPlay();
                Debug.Log("Монетка подобрана");
                gameObject.SetActive(false);
            }
        }
    }
}
