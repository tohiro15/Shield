using UnityEngine;

public class BarrierBullet : MonoBehaviour
{
    private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            other.GetComponent<ShieldController>().LoadBullet();
            Destroy(gameObject);
        }
    }

    public void Initialize(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
