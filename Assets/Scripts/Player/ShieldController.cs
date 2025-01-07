using Unity.VisualScripting;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public void ShieldOffset(Transform playerTransform, float distanceFromPlayer)
    {
        Vector3 offset = new Vector3(0, 0, distanceFromPlayer);
        transform.position = playerTransform.position + offset;
    }
    public void RotateAroundPlayer(Transform player, float rotateSpeed)
    {
        transform.RotateAround(player.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
