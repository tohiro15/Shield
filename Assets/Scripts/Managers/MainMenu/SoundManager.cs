using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer _musicAudioMixer;
    [SerializeField] private AudioMixer _sfxAudioMixer;
    [Header("Music Clips")]
    [SerializeField] private AudioClip _mainMenuMusicClip;
    [SerializeField] private AudioClip[] _levelMusicClips;
    [SerializeField] private AudioSource _musicAudioSource;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        AudioClip newClip = GetMusicForScene(sceneName);
        if (newClip != null)
        {
            PlayMusic(newClip);
        }
        else
        {
            Debug.LogWarning($"Музыка для сцены {sceneName} не найдена!");
        }
    }

    private AudioClip GetMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                return _mainMenuMusicClip;
            case "Level_1":
                return _levelMusicClips[0];
            case "Level_2":
                return _levelMusicClips[1];
            case "Level_3":
                return _levelMusicClips[2];
            default:
                return null;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_musicAudioSource == null)
        {
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        _musicAudioSource.Stop();
        _musicAudioSource.clip = clip;
        _musicAudioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        _musicAudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxAudioMixer.SetFloat("SFXVolume", volume);
    }

    public float GetMusicVolume()
    {
        if (_musicAudioMixer.GetFloat("MusicVolume", out float value))
        {
            return value;
        }
        return 0f;
    }

    public float GetSFXVolume()
    {
        if (_sfxAudioMixer.GetFloat("SFXVolume", out float value))
        {
            return value;
        }
        return 0f;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
