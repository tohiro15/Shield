using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;

    private Transform[] _checkpoints;

    private void Awake()
    {
        _player = FindFirstObjectByType<Player>();

        PopulateCheckpoints();
    }

    private void Start()
    {
        RegisterCheckpoints();
        SetPlayerStartPosition();
    }
    private void PopulateCheckpoints()
    {
        Checkpoint[] checkpointComponents = GetComponentsInChildren<Checkpoint>();

        if (checkpointComponents == null || checkpointComponents.Length == 0)
        {
            Debug.LogError("��������� �� ������� � �������� �������� CheckpointManager!");
            return;
        }

        _checkpoints = new Transform[checkpointComponents.Length];

        for (int i = 0; i < checkpointComponents.Length; i++)
        {
            _checkpoints[i] = checkpointComponents[i].transform;
        }

        Debug.Log($"������� {checkpointComponents.Length} ���������� � ��������� � ������.");
    }


    private void RegisterCheckpoints()
    {
        for (int i = 0; i < _checkpoints.Length; i++)
        {
            if (_checkpoints[i].TryGetComponent(out Checkpoint checkpoint))
            {
                checkpoint.Initialize(i, _playerData);
            }
            else
            {
                Debug.LogError($"������ {_checkpoints[i].name} �� �������� ��������� Checkpoint!");
            }
        }
    }

    private void SetPlayerStartPosition()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;

        if (_playerData.TryGetLevelData(currentLevelName, out var levelData) && levelData.CurrentCheckpoint < _checkpoints.Length)
        {
            Transform checkpoint = _checkpoints[levelData.CurrentCheckpoint].transform;
            Vector3 startPosition = checkpoint.position;

            _player.transform.position = new Vector3(startPosition.x, _player.transform.position.y, startPosition.z);
        }
        else
        {
            Debug.LogWarning("��� ������ ���������. ����� ����� ���������� � ��������� �������.");
            _player.transform.position = _checkpoints.Length > 0
                ? _checkpoints[0].position
                : Vector3.zero;
        }
    }
}
