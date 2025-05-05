using UnityEngine;
using UnityEngine.Rendering;

public class StasisShell : MonoBehaviour
{
    private ShieldController _shieldController;
    private FireController _fireController;
    private Renderer _bulletRenderer;

    private bool _isPickup = false;

    private void Awake()
    {
        _shieldController = FindObjectOfType<ShieldController>();
        _fireController = FindObjectOfType<FireController>();

        _bulletRenderer = GetComponent<Renderer>();
        if (_bulletRenderer == null)
        {
            _bulletRenderer = GetComponentInChildren<Renderer>();
        }

        if (_shieldController == null)
        {
            Debug.LogError("Shield Controller не найден в сцене!");

            return;
        }

        if( _fireController == null)
        {
            Debug.LogError("Fire Controller не найден в сцене!");

            return;
        }

        if (_bulletRenderer == null)
        {
            Debug.LogError("Renderer не найден на объекте или его дочерних объектах!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Shield"))
        {
            return;
        }

        if (_isPickup)
        {
            Debug.Log("Снаряд уже собран!");
            return;
        }

        if (_shieldController == null || _fireController == null) return;

        CollectStasisShell();
    }

    private void CollectStasisShell()
    {
        _shieldController.LoadBullet();

        if (_bulletRenderer != null)
        {
            Color newColor = _bulletRenderer.material.color;
            newColor.a = 100 / 255f;
            _bulletRenderer.material.color = newColor;
        }

        _isPickup = true;
        Debug.Log("Стазис снаряд собран!");
    }
}