using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class DeathTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField, Tooltip("Tags that can activate the death trigger.")]
    private string[] _validTags;

    [SerializeField, Tooltip("Reference to the player data object.")]
    private PlayerData _playerData;

    private void Awake()
    {
        ValidateConfiguration();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTagValid(other.tag))
        {
            HandleDeath();
        }
    }

    private void ValidateConfiguration()
    {
        if (_validTags == null || _validTags.Length == 0)
        {
            Debug.LogWarning("The valid tags array is empty. DeathTrigger will not work as expected.", this);
        }

        if (_playerData == null)
        {
            Debug.LogError("PlayerData is not assigned in DeathTrigger. Please assign it in the inspector.", this);
            enabled = false;
        }
    }

    private bool IsTagValid(string tag)
    {
        foreach (string validTag in _validTags)
        {
            if (tag == validTag)
            {
                return true;
            }
        }
        return false;
    }

    private void HandleDeath()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (_playerData.LevelsData.TryGetValue(currentScene.name, out var levelData))
        {
            levelData.CurrentCoinsCollected = 0;
            _playerData.UpdateLevelData(currentScene.name, false, true, false);
        }
        else
        {
            Debug.LogWarning($"Level data for scene '{currentScene.name}' was not found in PlayerData.");
        }

        SceneManager.LoadScene(currentScene.name);
    }
}
