using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;

    public void Initialize(float speed)
    {
        _speed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
