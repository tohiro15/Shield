using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private int _checkpointIndex;
    private PlayerData _playerData;

    public void Initialize(int index, PlayerData playerData)
    {
        _checkpointIndex = index;
        _playerData = playerData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _playerData != null)
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            if (_playerData.TryGetLevelData(currentLevelName, out var levelData) && _checkpointIndex > levelData.CurrentCheckpoint)
            {
                levelData.CurrentCheckpoint = _checkpointIndex;
                Debug.Log($"Чекпоинт {_checkpointIndex} взят!");
                OnCheckpointReached?.Invoke(_checkpointIndex);
            }
        }
    }

    // Событие, которое можно использовать для подписки отображение надписи сбора чекпойнта
    public static event Action<int> OnCheckpointReached;
}
