using UnityEngine;

public class PlayerController : MonoBehaviour, IMovable
{
    [Header("Player Settings")]
    [SerializeField] internal float _movementSpeed;
    [SerializeField] internal float _evadeSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector3(moveX * _evadeSpeed, 0f, _movementSpeed);
    }
}