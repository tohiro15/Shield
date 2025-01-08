using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float direction = 0;
    private bool _canFire = false;

    public void Initialize(Transform playerTransform, float distanceFromPlayer)
    {
        if (playerTransform == null)
        {
            Debug.Log("Player Transform - is null!");
            return;
        }

        Vector3 offset = new Vector3(0, 0, distanceFromPlayer);
        transform.position = playerTransform.position + offset;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canFire)
        {
            Fire();
        }
    }
    public void RotateAroundPlayer(Transform player, float rotateSpeed)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1;
        }

        if (direction != 0)
        {
            transform.RotateAround(player.transform.position, Vector3.up, direction * rotateSpeed * Time.deltaTime);
        }
    }

    public void Fire()
    {
        _canFire = false;
        Debug.Log("ВЫСТРЕЛ!");
    }

    public void LoadBullet()
    {
        _canFire = true;
        Debug.Log("Пуля словлена!");
    }
}
