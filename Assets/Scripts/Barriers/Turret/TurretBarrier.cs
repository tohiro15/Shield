using UnityEngine;

public class TurretBarrier : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _turretBulletPrefab;
    [SerializeField] private Color _shieldNewColor; // Цвет щита после попадания

    private float _nextFireTime;
    private Renderer _shieldRenderer;

    private void Start()
    {
        if (_turretBulletPrefab == null)
        {
            Debug.LogError("Turret Bullet Prefab is not assigned!", this);
            enabled = false;
            return;
        }

        _shieldRenderer = GetComponent<Renderer>();
        if (_shieldRenderer == null)
        {
            Debug.LogWarning("No Renderer found on Turret Barrier. Color change will not work.");
        }
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

    private void FireTurret()
    {
        GameObject newBullet = Instantiate(_turretBulletPrefab, _muzzle.position, _muzzle.rotation);
        if (newBullet == null)
        {
            Debug.LogError("Failed to instantiate the turret bullet.", this);
            return;
        }

        BarrierBullet bullet = newBullet.GetComponent<BarrierBullet>();
        if (bullet != null)
        {
            bullet.SetSpeed(_bulletSpeed);
        }
        else
        {
            Debug.LogError("BarrierBullet component not found on the instantiated bullet.", this);
        }
    }
    public void ChangeShieldColor() // Смена цвета щита при попадании в него пулей
    {
        if (_shieldRenderer != null)
        {
            _shieldRenderer.material.color = _shieldNewColor;
        }
        else
        {
            Debug.LogWarning("No Renderer attached, can't change shield color.");
        }
    }
}
