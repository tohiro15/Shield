using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject _bulletPrefab;
    private Material _shieldMaterial;
    private Transform _bulletSpawn;

    private Color _defaultColor;
    private Color _attackColor;

    private string[] _validTags;
    private float _bulletSpeed;

    private bool _canFire = false;

    public void Initialize(GameObject playerBulletPrefab, Material shieldMaterial, Transform bulletSpawnTransform, Color attackColor, string[] validTags, float bulletSpeed)
    {
        _bulletPrefab = playerBulletPrefab;
        _shieldMaterial = shieldMaterial;

        _bulletSpawn = bulletSpawnTransform;

        _defaultColor = shieldMaterial.color;

        _attackColor = attackColor;

        _bulletSpeed = bulletSpeed;

        _validTags = new string[validTags.Length];
        for (int i = 0; i < validTags.Length; i++)
        {
            _validTags[i] = validTags[i];
        }
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

        _shieldMaterial.color = _defaultColor;

        _canFire = false;
    }
    public void LoadBullet()
    {
        Debug.Log("Пуля словлена!");
        _shieldMaterial.color = _attackColor;
        _canFire = true;
    }

}
