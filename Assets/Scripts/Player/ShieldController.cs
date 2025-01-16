using Unity.VisualScripting;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float direction = 0;

    public void Initialize(Transform player, Transform shield, float distanceFromPlayer)
    {
        if (player == null)
        {
            Debug.Log("Player Transform - is null!");
            return;
        }

        Vector3 offset = new Vector3(0, 0, distanceFromPlayer);
        shield.position = player.position + offset;
    }
    public void RotateAroundPlayer(Transform player, Transform shield, float rotateSpeed)
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0) direction = -1;
        else if (horizontalInput > 0) direction = 1;

        if (direction != 0) shield.RotateAround(player.transform.position, Vector3.up, direction * rotateSpeed * Time.deltaTime);
    }
}
