using System;
using UnityEngine;

public class ShieldController : MonoBehaviour, IShield
{
    [Header("Shield Settings")]
    [SerializeField] private Transform _shieldTransform;
    [SerializeField] private Renderer _shieldRenderer;
    [SerializeField] private Material _defaultShieldMaterial;
    [SerializeField] private Material _attackShieldMaterial;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _shieldRotateSpeed;

    public Action BulletLoaded;

    private int _direction = 0;

    public void Initialize()
    {
        if (_shieldRenderer == null)
        {
            Debug.LogError("Shield Renderer - not found!");
            _shieldRenderer = _shieldTransform?.GetComponent<Renderer>();
        }

        if (_defaultShieldMaterial == null)
        {
            Debug.LogError("Default Shield Material - not found!");
        }

        if (_attackShieldMaterial == null)
        {
            Debug.LogError("Attack Shield Material - not found!");
        }

        if (_shieldTransform == null)
        {
            Debug.LogError("Shield Transform - not found!");
        }

        Vector3 offset = new Vector3(0, 0.1f, _distanceFromPlayer);
        _shieldTransform.position = transform.position + offset;

        _shieldRenderer.material = _defaultShieldMaterial;

        _shieldTransform.gameObject.SetActive(true);
    }
    public void SetDefaultMaterial()
    {
        _shieldRenderer.material = _defaultShieldMaterial;
    }
    public void LoadBullet()
    {
        _shieldRenderer.material = _attackShieldMaterial;

        BulletLoaded?.Invoke();
    }

    public void RotateAroundPlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0) _direction = -1;
        else if (horizontalInput > 0) _direction = 1;

        if (_direction != 0)
        {
            _shieldTransform.RotateAround(_targetTransform.position, Vector3.up, _direction * _shieldRotateSpeed);
        }
    }
}
