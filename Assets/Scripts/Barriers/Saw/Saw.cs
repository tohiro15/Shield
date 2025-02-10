using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Tooltip("Points between which the saw will move.")]
    private Transform[] _points;

    [SerializeField, Tooltip("Speed of the saw movement.")]
    private float _speed = 2f;

    private int _currentPointIndex = 0;

    private void Start()
    {
        ValidatePoints();
    }

    private void Update()
    {
        MoveTowardsNextPoint();
    }

    private void ValidatePoints()
    {
        if (_points == null || _points.Length < 2)
        {
            Debug.LogError("Saw requires at least two points to move between!", this);
            enabled = false;
        }
    }

    private void MoveTowardsNextPoint()
    {
        if (_points.Length == 0) return;

        Transform targetPoint = _points[_currentPointIndex];
        Vector3 targetPosition = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        if (HasReachedTarget(targetPosition))
        {
            UpdateTargetPoint();
        }
    }

    private bool HasReachedTarget(Vector3 targetPosition)
    {
        return Vector3.SqrMagnitude(transform.position - targetPosition) < 0.01f;
    }

    private void UpdateTargetPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
    }

    private void OnDrawGizmos()
    {
        if (_points == null || _points.Length < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < _points.Length; i++)
        {
            Transform currentPoint = _points[i];
            Transform nextPoint = _points[(i + 1) % _points.Length];

            if (currentPoint != null && nextPoint != null)
            {
                Gizmos.DrawLine(currentPoint.position, nextPoint.position);
            }
        }
    }
}
