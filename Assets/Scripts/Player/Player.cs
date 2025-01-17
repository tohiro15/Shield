using UnityEngine;

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

        _playerController = GetComponent<PlayerController>();

        _shieldController = GetComponent<ShieldController>();
        _shieldController.Initialize(transform, _shieldTransform, _distanceFromPlayer);

        _fireController = GetComponent<FireController>();
        _fireController.Initialize(_bulletPrefab, _bulletSpawnTransform, _validTags, _bulletSpeed);
    }
    void FixedUpdate()
    {
        _playerController.Movement(_movementSpeed, _evadeSpeed, _rigidbody);
    }
    private void Update()
    {
        _shieldController.RotateAroundPlayer(transform, _shieldTransform, _shieldRotateSpeed);
    }
}
