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

    public void SaveVolume(string parameter, float volume)
    {
        PlayerPrefs.SetFloat(parameter, volume);
    }

    public void LoadVolume(string parameter)
    {
        float volume = PlayerPrefs.GetFloat(parameter, 0f);
        SetVolume(parameter, volume);
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        audioMixer.SetFloat("MasterVolume", savedVolume);
    }
}
