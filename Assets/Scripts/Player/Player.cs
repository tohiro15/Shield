using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player Controllers")]
    [Space]

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ShieldController _shieldController;
    [SerializeField] private FireController _fireController;

    [Header ("Player Mod's")]
    [Space]

    [SerializeField] private bool _fireMode = true;
    [SerializeField] private bool _shieldMode = true;

    private IFire _fire;
    private IMovable _movement;
    private IShield _shield;

    private void Start()
    {
        if (_playerController != null)
        {
            _movement = _playerController;
        }

        if (!_shieldMode) _fireMode = false;

        if (_shieldController != null && _fireController != null && _fireMode && _shieldMode)
        {
            _fire = _fireController;
            _fireController.Initialize(_shieldController);
        }

        if (_shieldController != null && _shieldMode)
        {
            _shield = _shieldController;
            _shield.Initialize();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fire?.Fire();
        }

        _shield?.RotateAroundPlayer();
    }

    private void FixedUpdate()
    {
        _movement?.Movement();
    }
}
