using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsUI : MonoBehaviour
{
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    private void Start()
    {
        // Poner los sliders en el valor actual (guardado)
        sliderMaster.value = AudioManager.Instance.GetMasterVolume();
        sliderMusic.value = AudioManager.Instance.GetMusicVolume();
        sliderSFX.value = AudioManager.Instance.GetSFXVolume();

        // Conectar eventos
        sliderMaster.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}

