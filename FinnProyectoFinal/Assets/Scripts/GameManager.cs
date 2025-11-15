using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HUD hud;
    public static int lifes = 5;
    public static int maxLifes = 5;
    public static GameManager Instance { get; private set; }

    public int TotalPoints { get { return totalPoints; } }
    public int TotalCoins { get { return totalCoins; } }
    public int TotalCoinsGeneral { get { return totalCoinsGeneral; } }
    public int MaxPoint { get { return maxPoint; } }
    public int NowPoint { get { return nowPoint; } }

    public static int maxPoint = 15000;
    public static int nowPoint = 0;
    public static int totalPoints;
    public static int totalCoins;
    public static int totalCoinsGeneral = 5000;

    // -------- Valores base (modificados por Upgrades) --------
    public static float degradeLife = 10f;
    public static float speedPlayer = 4f;

    // -------- Bonos SOLO de esta partida (pickups) --------
    public static float runDamageBonus = 0f;
    public static float runSpeedBonus = 0f;

    // -------- Valores finales usados en el juego --------
    public static float CurrentDamage { get { return degradeLife + runDamageBonus; } }
    public static float CurrentSpeed { get { return speedPlayer + runSpeedBonus; } }

    public AudioClip damageProtagonist;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }

    // Llamar al iniciar / terminar una partida
    public static void ResetRunBonuses()
    {
        runDamageBonus = 0f;
        runSpeedBonus = 0f;
    }

    public void DecreaseLife()
    {
        lifes -= 1;
        SoundManager.Instance.audioSource.PlayOneShot(damageProtagonist, 1f);
        if (lifes == 0)
        {
            SceneManager.LoadScene("GameOver");
            AddCoins();
            addNowPoint();
            lifes = 5;
        }
        hud.DesactiveLife(lifes);
    }

    public bool AddLife()
    {
        if (lifes == maxLifes)
        {
            return false;
        }
        hud.ActiveLife(lifes);
        lifes += 1;
        return true;
    }

    public void AddPoint(int addPoint)
    {
        totalPoints += addPoint;
    }

    public void AddCoins()
    {
        totalCoins += (totalPoints * 5) / 100;
        AddTotalCoins();
        Debug.Log(totalCoins);
    }

    public void AddTotalCoins()
    {
        totalCoinsGeneral += totalCoins;
        Debug.Log(totalCoinsGeneral);
    }

    public void addNowPoint()
    {
        if (totalPoints > nowPoint)
        {
            nowPoint = totalPoints;
        }
        else if (nowPoint > maxPoint)
        {
            maxPoint = totalPoints;
        }
    }
}

