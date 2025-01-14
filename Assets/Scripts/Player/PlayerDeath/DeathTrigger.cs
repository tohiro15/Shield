using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private string[] _validTags;

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in _validTags)
        {
            if (other.CompareTag(tag))
            {
                ReloadScene();
                break;
            }
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

