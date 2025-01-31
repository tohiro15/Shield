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
                Debug.Log("������ ��� ������!");
                return;
            }
            if (_fireController == null)
            {
                Debug.LogError("FireController �� ����������!");
                return;
            }

            if (_bulletRenderer == null)
            {
                Debug.LogError("BulletRenderer �� ����������!");
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
                Debug.LogError("�������� �� ������ � BulletRenderer!");
            }

            _isPickup = true;

            Debug.Log("������ ������ ������!");
        }
    }

}
