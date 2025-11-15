using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI label;   // arrastra tu Text TMP aquí
    public float intervalo = 0.5f;  // cada cuánto actualizar

    int frames = 0;
    float acumulado = 0f;

    void Update()
    {
        frames++;
        acumulado += Time.unscaledDeltaTime;

        if (acumulado >= intervalo)
        {
            float fps = frames / acumulado;
            if (label != null) label.text = $"{fps:0.#} FPS";
            frames = 0;
            acumulado = 0f;
        }
    }
}
