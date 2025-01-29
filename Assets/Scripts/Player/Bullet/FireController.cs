using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject _bulletPrefab;
    private Material _defaultShieldMaterial;
    private Material _atttackShieldMaterial;
    private Transform _bulletSpawn;

    private Renderer _shieldMaterials;

    private string[] _validTags;
    private float _bulletSpeed;

    private bool _canFire = false;

    public void Initialize(Renderer shieldRenderer, GameObject playerBulletPrefab, Material defaultShieldMaterial, Material attackShieldMaterial, Transform bulletSpawnTransform,string[] validTags, float bulletSpeed)
    {
        _shieldMaterials = shieldRenderer;

        _bulletPrefab = playerBulletPrefab;

        _defaultShieldMaterial = defaultShieldMaterial;
        _shieldMaterials.material = _defaultShieldMaterial;

        _atttackShieldMaterial = attackShieldMaterial;

        _bulletSpawn = bulletSpawnTransform;

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

        _shieldMaterials.material = _defaultShieldMaterial;

        _canFire = false;
    }
    public void LoadBullet()
    {
        Debug.Log("Пуля словлена!");
        _shieldMaterials.material = _atttackShieldMaterial;
        _canFire = true;
    }

}
