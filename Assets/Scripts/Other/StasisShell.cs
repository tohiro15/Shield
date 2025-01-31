using UnityEngine;

public class StasisShell : MonoBehaviour
{
    [SerializeField] private FireController _fireController;
    [SerializeField] private Renderer _bulletRenderer;

    private bool _isPickup = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            if (_isPickup)
            {
                Debug.Log("Снаряд уже собран!");
                return;
            }
            if (_fireController == null)
            {
                Debug.LogError("FireController не установлен!");
                return;
            }

            if (_bulletRenderer == null)
            {
                Debug.LogError("BulletRenderer не установлен!");
                return;
            }

            _fireController.LoadBullet();

            if (_bulletRenderer.material != null)
            {
                Color newColor = _bulletRenderer.material.color;
                newColor.a = 100 / 255f;
                _bulletRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogError("Материал не найден у BulletRenderer!");
            }

            _isPickup = true;

            Debug.Log("Стазис снаряд собран!");
        }
    }

}
