using UnityEngine;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _evadeSpeed;

    [Header("Shield Settings")]
    [SerializeField] private bool _notShield;
    [SerializeField] private Transform _shieldTransform;
    [SerializeField] private Renderer _shieldRenderer;
    [SerializeField] private Material _defaultShieldMaterial;
    [SerializeField] private Material _attackShieldMaterial;
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _shieldRotateSpeed;

    [Header("Bullet Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private string[] _validTags;

    private PlayerController _playerController;
    private ShieldController _shieldController;
    private FireController _fireController;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();

        if (_notShield)
        {
            Debug.Log("Shield is disabled!");
            _shieldTransform?.gameObject.SetActive(false);
        }

        ValidateComponents();
        InitializeControllers();
    }

    private void ValidateComponents()
    {
        if (_shieldRenderer == null && !_notShield)
        {
            Debug.LogError("Shield Renderer not found! It will be initialized automatically.");
            _shieldRenderer = _shieldTransform?.GetComponent<Renderer>();
        }

        if (_defaultShieldMaterial == null && !_notShield)
        {
            Debug.LogError("Default Shield Material not found!");
        }

        if (_attackShieldMaterial == null && !_notShield)
        {
            Debug.LogError("Attack Shield Material not found!");
        }

        if (_shieldTransform == null && !_notShield)
        {
            Debug.LogError("Shield Transform not assigned!");
        }

        if (_bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab not assigned!");
        }

        if (_bulletSpawnTransform == null)
        {
            Debug.LogError("Bullet Spawn Transform not assigned!");
        }
    }

    private void InitializeControllers()
    {
        _playerController = GetComponent<PlayerController>() ?? throw new MissingComponentException("PlayerController not found!");

        if (!_notShield)
        {
            _shieldController = GetComponent<ShieldController>() ?? throw new MissingComponentException("ShieldController not found!");
            _shieldController.Initialize(transform, _shieldTransform, _distanceFromPlayer);

            _fireController = GetComponent<FireController>() ?? throw new MissingComponentException("FireController not found!");
            _fireController.Initialize(_shieldRenderer, _bulletPrefab, _defaultShieldMaterial, _attackShieldMaterial, _bulletSpawnTransform, _validTags, _bulletSpeed);
        }
    }

    private void FixedUpdate()
    {
        _playerController?.Movement(_movementSpeed, _evadeSpeed, _rigidbody);
    }

    private void Update()
    {
        if (!_notShield)
        {
            _shieldController?.RotateAroundPlayer(transform, _shieldTransform, _shieldRotateSpeed);
        }
    }
}