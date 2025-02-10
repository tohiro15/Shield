using UnityEngine;

public class StasisShell : MonoBehaviour
{
    private FireController _fireController;
    private Renderer _bulletRenderer;

    private bool _isPickup = false;

    private void Awake()
    {
        _fireController = FindObjectOfType<FireController>();

        _bulletRenderer = GetComponent<Renderer>();
        if (_bulletRenderer == null)
        {
            _bulletRenderer = GetComponentInChildren<Renderer>();
        }

        if (_fireController == null)
        {
            Debug.LogError("FireController не найден в сцене!");
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

        if (!ValidateComponents())
        {
            return;
        }

        CollectStasisShell();
    }

    private bool ValidateComponents()
    {
        if (_fireController == null)
        {
            Debug.LogError("FireController не установлен или не найден!");
            return false;
        }

        if (_bulletRenderer == null)
        {
            Debug.LogError("Renderer не найден на объекте или его дочерних объектах!");
            return false;
        }

        return true;
    }

    private void CollectStasisShell()
    {
        _fireController.LoadBullet();

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