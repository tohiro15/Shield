using UnityEngine;

public class BarrierBullet : MonoBehaviour
{
    private float _speed;
    private Vector3 _direction = Vector3.forward;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            FireController fireController = other.GetComponentInParent<FireController>();
            if (fireController != null)
            {
                fireController.LoadBullet();
            }
            else
            {
                Debug.LogWarning("FireController not found on parent of Shield!", this);
            }

            Destroy(gameObject); // Заменить на Object Pooling для оптимизации
        }
    }

    public void SetSpeed(float speed, Vector3 direction = default)
    {
        _speed = speed;
        _direction = direction == default ? Vector3.forward : direction.normalized;
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
