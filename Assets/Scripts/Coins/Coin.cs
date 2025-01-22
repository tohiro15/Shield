using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] private string[] _validTags;
    [SerializeField] private PlayerData _playerData;

    private Scene _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in _validTags)
        {
            if (other.CompareTag(tag))
            {
                _playerData.CoinPickUp(_currentScene.name);
                Debug.Log("Монетка подобрана");
                gameObject.SetActive(false);
            }
        }
    }
}
