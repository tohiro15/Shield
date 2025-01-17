using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2f;
    private int _currentPointIndex = 0;

    private void Start()
    {
        if (_points == null || _points.Length < 2)
        {
            Debug.LogError("Saw requires at least two points to move between!", this);
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        MoveTowardsNextPoint();
    }

    private void MoveTowardsNextPoint()
    {
        Transform targetPoint = _points[_currentPointIndex];

        Vector3 targetPosition = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (Vector3.SqrMagnitude(transform.position - targetPosition) < 0.01f)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
        }
    }
}
