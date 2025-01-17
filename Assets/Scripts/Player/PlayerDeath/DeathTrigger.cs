using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private string[] _validTags;

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
                ReloadScene();
                return;
            }
        }
    }

    private void ReloadScene()
    {
        // Получаем текущую сцену и загружаем ее заново
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
