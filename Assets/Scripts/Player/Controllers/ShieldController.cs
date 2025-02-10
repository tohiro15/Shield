using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float _direction = 0;

    public void Initialize(Transform player, Transform shield, float distanceFromPlayer)
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is null!");
            return;
        }

        Vector3 offset = new Vector3(0, 0.1f, distanceFromPlayer);
        shield.position = player.position + offset;
    }

    public void RotateAroundPlayer(Transform player, Transform shield, float rotateSpeed)
    {
        if (player == null || shield == null)
        {
            Debug.LogError("Player or Shield Transform is null!");
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0) _direction = -1;
        else if (horizontalInput > 0) _direction = 1;

        if (_direction != 0)
        {
            shield.RotateAround(player.position, Vector3.up, _direction * rotateSpeed * Time.deltaTime);
        }
    }
}
