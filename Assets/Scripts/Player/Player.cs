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

    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _shieldRotateSpeed;

    private PlayerController _playerController;
    private ShieldController _shieldController;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerController =GetComponent<PlayerController>();
        _shieldController = GetComponentInChildren<ShieldController>();

        _shieldController.ShieldOffset(transform, _distanceFromPlayer);
    }
    void FixedUpdate()
    {
        _playerController.Movement(_movementSpeed, _evadeSpeed, _rigidbody);
    }
    private void Update()
    {
        _shieldController.RotateAroundPlayer(transform, _shieldRotateSpeed);
    }
}
