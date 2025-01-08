using UnityEngine;

public class TurretBarrier : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bulletPrefab;

    private float _nextFireTime; 

    private void Update()
    {
        _nextFireTime -= Time.deltaTime;

        if (_nextFireTime <= 0)
        {
            FireTurret();
            _nextFireTime = _fireRate;
        }
    }

    public void FireTurret()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Initialize(_bulletSpeed);
        }
    }
}
