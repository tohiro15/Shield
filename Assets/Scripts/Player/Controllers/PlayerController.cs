using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void Movement(float movementSpeed, float evadeSpeed, Rigidbody rb)
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveX * evadeSpeed, 0f, movementSpeed);
    }
}
