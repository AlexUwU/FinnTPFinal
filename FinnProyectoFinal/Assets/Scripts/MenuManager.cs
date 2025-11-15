using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        // Reiniciar puntos de la run
        GameManager.totalPoints = 0;
        GameManager.totalCoins = 0;

        // Reiniciar bonos solo de esta partida
        GameManager.ResetRunBonuses();

        SceneManager.LoadScene("LevelOne");
    }

    public void Development()
    {
        SceneManager.LoadScene("Developents");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        // Por si se vuelve desde otra escena, reseteamos bonos de partida
        GameManager.ResetRunBonuses();

        SceneManager.LoadScene("StartGame");
    }

    public void changeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}

