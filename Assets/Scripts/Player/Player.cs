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

    [SerializeField] private bool _notShield;
    [SerializeField] private Transform _shieldTransform;
    [SerializeField] private Renderer _shieldRenderer;
    [SerializeField] private Material _defaultShieldMaterial;
    [SerializeField] private Material _attackShieldMaterial;
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
    private FireController _fireController;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_notShield)
        {
            Debug.Log("��� ��������!");
            _shieldTransform.gameObject.SetActive(false);
        }

        if (_shieldRenderer == null && _notShield == false)
        {
            Debug.LogError("Shield Renderer �� ������! ��������� ����� ��������������� �������������!");
            _shieldRenderer = _shieldTransform.gameObject.GetComponent<Renderer>();
        }

        if (_defaultShieldMaterial == null && _notShield == false)
        {
            Debug.LogError("Default Shield Material �� ������!");
        }

        if (_attackShieldMaterial == null && _notShield == false)
        {
            Debug.LogError("Attack Shield Material �� ������!");
        }

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
        if (_shieldController == null && _notShield == false)
        {
            Debug.LogError("ShieldController �� ������!");
        }
        else
        {
            _shieldController.Initialize(transform, _shieldTransform, _distanceFromPlayer);
        }

        _fireController = GetComponent<FireController>();
        if (_fireController == null && _notShield == false)
        {
            Debug.LogError("FireController �� ������!");
        }
        else if(_notShield == false)
        {
            _fireController.Initialize(_shieldRenderer, _bulletPrefab, _defaultShieldMaterial, _attackShieldMaterial, _bulletSpawnTransform, _validTags, _bulletSpeed);
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