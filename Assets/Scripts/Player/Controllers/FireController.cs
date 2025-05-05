using System;
using UnityEngine;

public class FireController : MonoBehaviour, IFire
{
    [Header("Fire Settings")]
    [Space]

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private string[] _validTags;

    [Header("Bullet Spawn Settings")]
    [Space]

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    
    public event Action OnFire;

    private ShieldController _shieldController;
    private bool _canFire = false;

    public void Initialize(ShieldController shield)
    {
        _shieldController = GetComponent<ShieldController>();

        if (_shieldController == null) return;
        else _shieldController.BulletLoaded += LoadBullet;
        if (_bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab - not found!");
        }

        if (_bulletSpawn == null)
        {
            Debug.LogError("Bullet Spawn Transform - not found!");
        }

        shield.BulletLoaded += EnableFire;
    }

    private void EnableFire()
    {
        _canFire = true;
    }

    public void LoadBullet()
    {
        _canFire = true;
    }
    public void Fire()
    {
        if (!_canFire || _shieldController == null) return;
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(_validTags, _bulletSpeed);
        }

        OnFire?.Invoke();

        _canFire = false;
    }
}
