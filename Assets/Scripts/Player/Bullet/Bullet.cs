using UnityEngine;

public class Bullet : MonoBehaviour
{
    private string[] _validTags;
    private float _bulletSpeed = 5f;
    public void Initialize(string[] validTags, float bulletSpeed)
    {
        _validTags = new string[validTags.Length];
        for (int i = 0; i < validTags.Length; i++)
        {
            _validTags[i] = validTags[i];
        }
        _bulletSpeed = bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in _validTags)
        {
            if (other.CompareTag(tag))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);
    }
}
