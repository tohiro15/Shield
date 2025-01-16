using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2f;
    private int _currentPointIndex = 0;

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Transform targetPoint = _points[_currentPointIndex];

        Vector3 targetPosition = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
        }
    }
}
