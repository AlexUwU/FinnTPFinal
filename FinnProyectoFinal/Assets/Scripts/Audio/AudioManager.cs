using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Fuentes de audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;

    // Nombres de parámetros del AudioMixer
    private const string MASTER_PARAM = "MasterVol";
    private const string MUSIC_PARAM = "MusicVol";
    private const string SFX_PARAM = "SFXVol";

    // Keys de PlayerPrefs
    private const string MASTER_KEY = "Vol_Master";
    private const string MUSIC_KEY = "Vol_Music";
    private const string SFX_KEY = "Vol_SFX";

    private void Awake()
    {
        // Singleton + persistente entre escenas
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Aplicar volúmenes guardados
        float master = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float music = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }

    // Convierte valor de slider (0–1) a dB para el mixer
    private float SliderToDb(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        return Mathf.Log10(value) * 20f;
    }

    // ---------- VOLUMEN ----------

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat(MASTER_PARAM, SliderToDb(value));
        PlayerPrefs.SetFloat(MASTER_KEY, value);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MUSIC_PARAM, SliderToDb(value));
        PlayerPrefs.SetFloat(MUSIC_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(SFX_PARAM, SliderToDb(value));
        PlayerPrefs.SetFloat(SFX_KEY, value);
    }

    public float GetMasterVolume() => PlayerPrefs.GetFloat(MASTER_KEY, 1f);
    public float GetMusicVolume() => PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
    public float GetSFXVolume() => PlayerPrefs.GetFloat(SFX_KEY, 1f);

    // ---------- MÚSICA ----------

    public void PlayMusic(int index, bool loop = true)
    {
        if (index < 0 || index >= musicClips.Length) return;

        musicSource.clip = musicClips[index];
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ---------- SFX ----------

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length) return;

        sfxSource.PlayOneShot(sfxClips[index]);
    }
}
