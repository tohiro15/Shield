using UnityEngine;

public class TurretBarrier : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Color _shieldNewColor;
    private float _nextFireTime;

    private void Start()
    {
        _nextFireTime = _fireRate;
    }

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
        BarrierBullet bullet = newBullet.GetComponent<BarrierBullet>();
        if (bullet != null)
        {
            bullet.Initialize(_bulletSpeed);
        }
    }
}
