using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private string[] _validTags;
    [SerializeField] private PlayerData _playerData;
    private void OnTriggerEnter(Collider other)
    {
        if (_validTags.Length == 0)
        {
            Debug.LogWarning("ValidTags array is empty in DeathTrigger.");
            return;
        }

        foreach (string tag in _validTags)
        {
            if (other.CompareTag(tag))
            {
                Death();
                return;
            }
        }
    }

    private void Death()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        _playerData.LevelsData[currentScene.name].CurrentCoinsCollected = 0;

        _playerData.UpdateLevelData(currentScene.name, false, true, false);
        SceneManager.LoadScene(currentScene.name);
    }
}
