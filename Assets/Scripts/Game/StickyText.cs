using System.Collections;
using TMPro;
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

        Vector3 startPosition = transform.position;

        while (elapsedTime < _delay)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, point.position.z);

            transform.position = targetPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, point.position.z);
    }
}

