using UnityEngine;

public class StasisShell : MonoBehaviour
{
    private FireController _fireController;
    private Renderer _bulletRenderer;
    private SpriteRenderer _spriteRenderer;

    private bool _isPickup = false;

    private void Awake()
    {
        _fireController = FindObjectOfType<FireController>();

        _bulletRenderer = GetComponent<Renderer>();
        if (_bulletRenderer == null)
        {
            _bulletRenderer = GetComponentInChildren<Renderer>();
        }

        if (_bulletRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }

        if (_fireController == null)
        {
            Debug.LogError("FireController �� ������ � �����!");
        }

        if (_bulletRenderer == null && _spriteRenderer == null)
        {
            Debug.LogError("�� Renderer, �� SpriteRenderer �� ������� �� ������� ��� ��� �������� ��������!");
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
            Debug.Log("������ ��� ������!");
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
            Debug.LogError("FireController �� ���������� ��� �� ������!");
            return false;
        }

        if (_bulletRenderer == null && _spriteRenderer == null)
        {
            Debug.LogError("�� BulletRenderer, �� SpriteRenderer �� ����������� ��� �� �������!");
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
        else if (_spriteRenderer != null)
        {
            Color newColor = _spriteRenderer.color;
            newColor.a = 100 / 255f;
            _spriteRenderer.color = newColor;
        }

        _isPickup = true;
        Debug.Log("������ ������ ������!");
    }
}
