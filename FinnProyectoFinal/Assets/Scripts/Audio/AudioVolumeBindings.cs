using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeBindings : MonoBehaviour
{
    [Header("Sliders (0-1)")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    void Start()
    {
        // Cargar valores guardados en los sliders
        sliderMaster.value = PlayerPrefs.GetFloat("VolumenMaestro", 1f);
        sliderMusic.value = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        sliderSFX.value = PlayerPrefs.GetFloat("VolumenFX", 1f);

        // Aplicar al iniciar
        AudioManager.Instance.SetMasterVolume(sliderMaster.value);
        AudioManager.Instance.SetMusicVolume(sliderMusic.value);
        AudioManager.Instance.SetSFXVolume(sliderSFX.value);

        // Listeners
        sliderMaster.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}



