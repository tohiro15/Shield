using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider barrier)
    {
        if(barrier.CompareTag("Barrier"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
    public void Movement(float movementSpeed, float evadeSpeed, Rigidbody rb)
    {
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(moveX * evadeSpeed, 0f, movementSpeed);
    }
}
