using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject _bulletPrefab;
    private Material _defaultShieldMaterial;
    private Material _attackShieldMaterial;
    private Transform _bulletSpawn;

    private Renderer _shieldRenderer;
    private string[] _validTags;
    private float _bulletSpeed;
    private bool _canFire = false;

    public void Initialize(Renderer shieldRenderer, GameObject bulletPrefab, Material defaultShieldMaterial, Material attackShieldMaterial, Transform bulletSpawnTransform, string[] validTags, float bulletSpeed)
    {
        _shieldRenderer = shieldRenderer;
        _bulletPrefab = bulletPrefab;
        _defaultShieldMaterial = defaultShieldMaterial;
        _attackShieldMaterial = attackShieldMaterial;
        _bulletSpawn = bulletSpawnTransform;
        _bulletSpeed = bulletSpeed;

        _validTags = (string[])validTags.Clone();

        _shieldRenderer.material = _defaultShieldMaterial;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(_validTags, _bulletSpeed);
        }

        _shieldRenderer.material = _defaultShieldMaterial;
        _canFire = false;
    }

    public void LoadBullet()
    {
        Debug.Log("Bullet captured!");
        _shieldRenderer.material = _attackShieldMaterial;
        _canFire = true;
    }
}
