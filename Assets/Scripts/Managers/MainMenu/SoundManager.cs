using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;
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

    [Header("Effect Clips")]
    [SerializeField] private AudioClip _coinPickupClip;
    [SerializeField] private AudioSource _sfxAudioSource;

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

    #region Music
    private void InitializeSceneMusicMap()
    {
        _sceneMusicMap = new Dictionary<string, AudioClip>(System.StringComparer.OrdinalIgnoreCase);

        // Add default mappings
        if (_mainMenuMusicClip != null) _sceneMusicMap["MainMenu"] = _mainMenuMusicClip;
        if (_developmentMusicClip != null) _sceneMusicMap["Level_0"] = _developmentMusicClip;

        // Add level-specific mappings
        for (int i = 0; i < _levelMusicClips.Length; i++)
        {
            if (_levelMusicClips[i] != null)
            {
                _sceneMusicMap[$"Level_{i + 1}"] = _levelMusicClips[i];
            }
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
            _musicAudioSource.clip = clip;
            _musicAudioSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, -80f, 0f);
        _musicAudioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, -80f, 0f);
        _sfxAudioMixer.SetFloat("SFXVolume", volume);
    }

    public float GetMusicVolume()
    {
        return GetVolume("MusicVolume", _musicAudioMixer);
    }

    public float GetSFXVolume()
    {
        return GetVolume("SFXVolume", _sfxAudioMixer);
    }

    private float GetVolume(string parameterName, AudioMixer mixer)
    {
        if (mixer.GetFloat(parameterName, out float value))
        {
            return value;
        }
        return 0f;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region Effects
    public void CoinPickupSoundPlay()
    {
        if (_sfxAudioSource != null)
        {
            _sfxAudioSource.PlayOneShot(_coinPickupClip);
        }
        else
        {
            Debug.LogWarning("SFX AudioSource is not assigned!");
        }
    }
    #endregion
}
