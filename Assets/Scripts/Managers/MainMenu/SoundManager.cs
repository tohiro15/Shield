using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer _musicAudioMixer;
    [SerializeField] private AudioMixer _sfxAudioMixer;

    [Header("Music Clips")]
    [SerializeField] private AudioClip _mainMenuMusicClip;
    [SerializeField] private AudioClip _developmentMusicClip;
    [SerializeField] private AudioClip[] _levelMusicClips;
    [SerializeField] private AudioSource _musicAudioSource;

    private Dictionary<string, AudioClip> _sceneMusicMap;

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

        InitializeSceneMusicMap();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitializeSceneMusicMap()
    {
        _sceneMusicMap = new Dictionary<string, AudioClip>
        {
            { "MainMenu", _mainMenuMusicClip },
            { "Development", _developmentMusicClip }
        };

        for (int i = 0; i < _levelMusicClips.Length; i++)
        {
            _sceneMusicMap.Add($"Level_{i + 1}", _levelMusicClips[i]);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (_sceneMusicMap.TryGetValue(sceneName, out AudioClip newClip))
        {
            PlayMusic(newClip);
        }
        else
        {
            Debug.LogWarning($"Музыка для сцены {sceneName} не найдена!");
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_musicAudioSource == null)
        {
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
        }

        if (_musicAudioSource.clip != clip)
        {
            _musicAudioSource.Stop();
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
        }
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
        return GetVolume("MusicVolume");
    }

    public float GetSFXVolume()
    {
        return GetVolume("SFXVolume");
    }

    private float GetVolume(string parameterName)
    {
        if (_musicAudioMixer.GetFloat(parameterName, out float value))
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
