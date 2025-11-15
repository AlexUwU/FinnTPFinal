using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpcionesMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private Slider sliderVolumenMaestro;
    [SerializeField] private Slider sliderVolumenMusica;
    [SerializeField] private Slider sliderVolumenFX;

    [Header("Pantalla y Video")]
    [SerializeField] private Toggle toggleFullScreen;
    [SerializeField] private TMP_Dropdown dropdownResolucion;
    [SerializeField] private Toggle toggleVsync;
    [SerializeField] private TMP_Dropdown dropdownFrameRate;
    [SerializeField] private Slider sliderBrillo;
    [SerializeField] private TMP_Dropdown dropdownCalidad;

    [Tooltip("Opcional: Image a pantalla completa (negro) para simular brillo. Nombre sugerido: 'BrilloOverlay'")]
    [SerializeField] private Image brilloOverlay;

    private Resolution[] resoluciones;
    private readonly List<string> opcionesFrameRate = new List<string> { "30 Hz", "60 Hz", "120 Hz", "Ilimitado" };

    private void Awake()
    {
        // Si no se asignó por Inspector, intenta encontrar el overlay por nombre
        if (brilloOverlay == null)
        {
            var go = GameObject.Find("BrilloOverlay");
            if (go != null) brilloOverlay = go.GetComponent<Image>();
        }
    }

    private void Start()
    {
        // ---------- Dropdown Resolución ----------
        resoluciones = Screen.resolutions;
        var opcionesRes = new List<string>();
        int indiceActual = 0;

        dropdownResolucion.ClearOptions();

        // Evitar resoluciones repetidas (mismo WxH con distintos Hz)
        var vistos = new HashSet<string>();
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string etiqueta = $"{resoluciones[i].width}x{resoluciones[i].height}";
            if (vistos.Add(etiqueta))
            {
                opcionesRes.Add(etiqueta);
                if (resoluciones[i].width == Screen.currentResolution.width &&
                    resoluciones[i].height == Screen.currentResolution.height)
                {
                    indiceActual = opcionesRes.Count - 1;
                }
            }
        }

        dropdownResolucion.AddOptions(opcionesRes);
        dropdownResolucion.value = PlayerPrefs.GetInt("Resolucion", indiceActual);
        dropdownResolucion.RefreshShownValue();
        dropdownResolucion.onValueChanged.AddListener(CambiarResolucion);

        // ---------- Dropdown FrameRate ----------
        dropdownFrameRate.ClearOptions();
        dropdownFrameRate.AddOptions(opcionesFrameRate);
        dropdownFrameRate.value = PlayerPrefs.GetInt("FrameRate", 1); // por defecto 60 Hz
        dropdownFrameRate.RefreshShownValue();
        dropdownFrameRate.onValueChanged.AddListener(AplicarFrameRate);

        // ---------- Dropdown Calidad ----------
        dropdownCalidad.ClearOptions();
        dropdownCalidad.AddOptions(new List<string>(QualitySettings.names));
        int calidadGuardada = PlayerPrefs.GetInt("Calidad", QualitySettings.GetQualityLevel());
        calidadGuardada = Mathf.Clamp(calidadGuardada, 0, QualitySettings.names.Length - 1);
        dropdownCalidad.value = calidadGuardada;
        dropdownCalidad.RefreshShownValue();
        dropdownCalidad.onValueChanged.AddListener(CambiarCalidad);

        // ---------- Toggles ----------
        toggleFullScreen.isOn = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        toggleFullScreen.onValueChanged.AddListener(AplicarPantallaCompleta);

        toggleVsync.isOn = PlayerPrefs.GetInt("Vsync", 1) == 1;
        toggleVsync.onValueChanged.AddListener(AplicarVsync);

        // ---------- Sliders ----------
        sliderVolumenMaestro.value = PlayerPrefs.GetFloat("VolumenMaestro", 1f);
        sliderVolumenMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        sliderVolumenFX.value = PlayerPrefs.GetFloat("VolumenFX", 1f);
        sliderBrillo.value = PlayerPrefs.GetFloat("Brillo", 1f);

        sliderVolumenMaestro.onValueChanged.AddListener(_ => AplicarVolumen());
        sliderVolumenMusica.onValueChanged.AddListener(_ => AplicarVolumen());
        sliderVolumenFX.onValueChanged.AddListener(_ => AplicarVolumen());
        sliderBrillo.onValueChanged.AddListener(AplicarBrillo);

        // ---------- Aplicar todo al inicio ----------
        AplicarVolumen();
        AplicarPantallaCompleta(toggleFullScreen.isOn);
        CambiarResolucion(dropdownResolucion.value);
        AplicarVsync(toggleVsync.isOn);
        AplicarFrameRate(dropdownFrameRate.value);
        CambiarCalidad(dropdownCalidad.value);
        AplicarBrillo(sliderBrillo.value);

        PlayerPrefs.Save();
    }

    // ================== AUDIO ==================
    public void AplicarVolumen()
    {
        // Volumen maestro global (si usas AudioMixer, cambia esto por Mixer.SetFloat)
        AudioListener.volume = sliderVolumenMaestro.value;

        PlayerPrefs.SetFloat("VolumenMaestro", sliderVolumenMaestro.value);
        PlayerPrefs.SetFloat("VolumenMusica", sliderVolumenMusica.value);
        PlayerPrefs.SetFloat("VolumenFX", sliderVolumenFX.value);
        PlayerPrefs.Save();
    }

    // ================== VIDEO ==================
    public void AplicarPantallaCompleta(bool activo)
    {
        Screen.fullScreen = activo;
        PlayerPrefs.SetInt("FullScreen", activo ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void CambiarResolucion(int indice)
    {
        // Buscar la primera resolución que coincida con la etiqueta del dropdown
        string etiqueta = dropdownResolucion.options[indice].text;
        foreach (var r in resoluciones)
        {
            if ($"{r.width}x{r.height}" == etiqueta)
            {
                Screen.SetResolution(r.width, r.height, Screen.fullScreen);
                break;
            }
        }
        PlayerPrefs.SetInt("Resolucion", indice);
        PlayerPrefs.Save();
    }

    public void AplicarVsync(bool activo)
    {
        QualitySettings.vSyncCount = activo ? 1 : 0;

        // Si VSync está activo, deja sin límite el targetFrameRate
        if (activo) Application.targetFrameRate = -1;
        else AplicarFrameRate(dropdownFrameRate.value);

        PlayerPrefs.SetInt("Vsync", activo ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void AplicarFrameRate(int indice)
    {
        // Si VSync está ON, no fuerces un cap (lo limita el monitor)
        if (QualitySettings.vSyncCount > 0)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            switch (indice)
            {
                case 0: Application.targetFrameRate = 30; break;
                case 1: Application.targetFrameRate = 60; break;
                case 2: Application.targetFrameRate = 120; break;
                default: Application.targetFrameRate = -1; break; // Ilimitado
            }
        }

        PlayerPrefs.SetInt("FrameRate", indice);
        PlayerPrefs.Save();
    }

    public void AplicarBrillo(float valor)
    {
        // Opción 1: Overlay de UI (recomendado)
        if (brilloOverlay != null)
        {
            // valor 1 = sin oscurecer; 0 = negro total
            // Convertimos a "oscurecer": alpha = 1 - valor
            var c = brilloOverlay.color;
            c.a = Mathf.Clamp01(1f - valor);
            brilloOverlay.color = c;
        }
        else
        {
            // Opción 2 (fallback): luz ambiental
            RenderSettings.ambientLight = Color.white * Mathf.LinearToGammaSpace(Mathf.Clamp01(valor));
        }

        PlayerPrefs.SetFloat("Brillo", valor);
        PlayerPrefs.Save();
    }

    public void CambiarCalidad(int indice)
    {
        indice = Mathf.Clamp(indice, 0, QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(indice, true);
        PlayerPrefs.SetInt("Calidad", indice);
        PlayerPrefs.Save();
    }
}


