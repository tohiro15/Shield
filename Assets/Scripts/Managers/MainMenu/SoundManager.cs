using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(string parameter, float volume)
    {
        audioMixer.SetFloat(parameter, volume);
    }

    public float GetVolume(string parameter)
    {
        if (audioMixer.GetFloat(parameter, out float value))
        {
            return value;
        }
        return 0f;
    }
}
