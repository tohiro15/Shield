using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private HashSet<string> _validTags;
    private float _bulletSpeed = 5f;

    public void Initialize(string[] validTags, float bulletSpeed)
    {
        if (validTags == null || validTags.Length == 0)
        {
            Debug.LogError("Нет валидных тегов для пули!");
            return;
        }
        _validTags = new HashSet<string>(validTags);
        _bulletSpeed = bulletSpeed > 0 ? bulletSpeed : 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_validTags.Contains(other.tag))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);
    }
}
