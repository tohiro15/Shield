using UnityEngine;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [Space]

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _evadeSpeed;

    [Header("Shield Settings")]
    [Space]

    [SerializeField] private Transform _shieldTransform;
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _shieldRotateSpeed;

    [Header("Bullet Settings")]
    [Space]

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private string[] _validTags;

    private PlayerController _playerController;
    private ShieldController _shieldController;
    public FireController _fireController;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody �� ������! ��������� ����� �������� �������������.");
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        _playerController = GetComponent<PlayerController>();
        if (_playerController == null)
        {
            Debug.LogError("PlayerController �� ������!");
        }

        _shieldController = GetComponent<ShieldController>();
        if (_shieldController == null)
        {
            Debug.LogError("ShieldController �� ������!");
        }
        else
        {
            _shieldController.Initialize(transform, _shieldTransform, _distanceFromPlayer);
        }

        _fireController = GetComponent<FireController>();
        if (_fireController == null)
        {
            Debug.LogError("FireController �� ������!");
        }
        else
        {
            _fireController.Initialize(_bulletPrefab, _bulletSpawnTransform, _validTags, _bulletSpeed);
        }
    }

    void FixedUpdate()
    {
        if (_playerController != null)
        {
            _playerController.Movement(_movementSpeed, _evadeSpeed, _rigidbody);
        }
    }

    private void Update()
    {
        if (_shieldController != null)
        {
            _shieldController.RotateAroundPlayer(transform, _shieldTransform, _shieldRotateSpeed);
        }
    }
}
