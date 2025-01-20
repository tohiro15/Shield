using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StickyText : MonoBehaviour
{
    [SerializeField] private float _delay = 2f;

    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.size = new Vector3(50, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TextDelay"))
        {
            StartCoroutine(TextDelayOnPoint(other.transform));
        }
    }
    private IEnumerator TextDelayOnPoint(Transform point)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _delay)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, point.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / _delay);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

