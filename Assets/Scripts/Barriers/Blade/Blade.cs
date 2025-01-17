using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private Transform _blade;
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _offset = 3f;
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
        Vector3 offset = randomSide ? new Vector3(0, 0, -_offset) : new Vector3(0, 0, _offset);
        _blade.position = _centerPoint.position + offset;

        _isClockwise = randomSide;
    }

    private void Update()
    {
        RotateAroundPoint();
    }

    private void RotateAroundPoint()
    {
        Vector3 rotationAxis = _isClockwise ? Vector3.down : Vector3.up;
        _blade.RotateAround(_centerPoint.position, rotationAxis, _rotationSpeed * Time.deltaTime);
    }
}
