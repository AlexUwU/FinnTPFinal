using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject[] lifes;
    public GameManager gameManager;
    public TextMeshProUGUI point;
    public TextMeshProUGUI coins;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI damageText;

    void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        // Puntos y monedas
        point.text = gameManager.TotalPoints.ToString();
        coins.text = gameManager.TotalCoins.ToString();

        // Mostrar VELOCIDAD (base + bonos)
        speedText.text = $"{GameManager.CurrentSpeed:F1}";

        // Mostrar DAÑO (base + bonos)
        damageText.text = $"{GameManager.CurrentDamage:F1}";
    }

    public void DesactiveLife(int i)
    {
        lifes[i].SetActive(false);
    }

    public void ActiveLife(int i)
    {
        lifes[i].SetActive(true);
    }
}

