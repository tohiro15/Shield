using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float direction = 0;
    public void ShieldOffset(Transform playerTransform, float distanceFromPlayer)
    {
        if(playerTransform == null) 
        {
            Debug.Log("Player Transform - is null!");
            return; 
        }

        Vector3 offset = new Vector3(0, 0, distanceFromPlayer);
        transform.position = playerTransform.position + offset;
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

        if(direction != 0)
        {
            transform.RotateAround(player.transform.position, Vector3.up, direction * rotateSpeed * Time.deltaTime);
        }
    }
}
