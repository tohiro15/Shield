using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StickyText : MonoBehaviour
{
    [Header("Adhesion parameters")]
    [Space]
    [SerializeField] private float _delay = 2f;

    private BoxCollider _boxCollider;

    [Header("Transparency Settings")]
    [Space]
    [SerializeField] private float _duration = 1.0f;

    private TextMeshProUGUI _textMeshPro;
    private Color _targetColorStart;
    private Color _targetColorFinish;
    private Color _initialColor;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.size = new Vector3(50, 0, 0);

        _textMeshPro = GetComponent<TextMeshProUGUI>();

        _textMeshPro.color = new Color(_textMeshPro.color.r, _textMeshPro.color.g, _textMeshPro.color.b, 0);
        _initialColor = _textMeshPro.color;

        _targetColorStart = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 1f);
        _targetColorFinish = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0f);
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
        StartCoroutine(FadeTextAlpha(_targetColorStart));

        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < _delay)
        {
            transform.position = Vector3.Lerp(initialPosition, new Vector3(transform.position.x, transform.position.y, point.position.z), 1f);
            elapsedTime += Time.deltaTime;
            yield return null;

            _boxCollider.enabled = false;

        }
            StartCoroutine(FadeTextAlpha(_targetColorFinish));
    }

    private IEnumerator FadeTextAlpha(Color targetColor)
    {
        float timeElapsed = 0f;
        Color initialColor = _textMeshPro.color;

        while (timeElapsed < _duration)
        {
            _textMeshPro.color = Color.Lerp(initialColor, targetColor, timeElapsed / _duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _textMeshPro.color = targetColor;
    }
}
