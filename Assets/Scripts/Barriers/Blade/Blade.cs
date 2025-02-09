using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Blade : MonoBehaviour
{
    [SerializeField] private Transform _blade;
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private bool _isClockwise = true; // true - по часовой, false - против часовой

    private void Start()
    {
        if (_centerPoint == null)
        {
            Debug.LogError("Center point is not assigned!", this);
            enabled = false;
            return;
        }
        if (_blade == null)
        {
            Debug.LogError("Blade is not assigned!", this);
            enabled = false;
            return;
        }

        bool randomSide = Random.Range(0, 2) == 1;

        _isClockwise = randomSide;
    }

    private void FixedUpdate()
    {
        RotateAroundPoint();
    }

    private void RotateAroundPoint()
    {
        Vector3 rotationAxis = _isClockwise ? Vector3.down : Vector3.up;

        _centerPoint.Rotate(rotationAxis, _rotationSpeed * Time.deltaTime);
    }
}
