using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject _bulletPrefab;
    private Transform _bulletSpawn;

    private string[] _validTags;
    private float _bulletSpeed;

    private bool _canFire;

    public void Initialize(GameObject playerBulletPrefab, Transform bulletSpawnTransform, string[] validTags, float bulletSpeed)
    {
        _bulletPrefab = playerBulletPrefab;
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
        newBullet.GetComponent<Bullet>().Initialize(_validTags, _bulletSpeed);
        _canFire = false;
    }
    public void LoadBullet()
    {
        Debug.Log("Пуля словлена!");
        _canFire = true;
    }

}
