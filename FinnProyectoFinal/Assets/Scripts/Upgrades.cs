using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public GameManager gameManager;
    public HUD hud;
    public TextMeshProUGUI coinsGeneral;
    public TextMeshProUGUI recordMax;
    public TextMeshProUGUI recordNow;
    private int upgradeVLevel = 0;
    private int upgradeLLevel = 0;
    private int upgradeDLevel = 0;

    private static int priceV = 1000;
    private static int priceL = 100000;
    private static int priceD = 1500;

    public TextMeshProUGUI priceVe;
    public TextMeshProUGUI priceLi;
    public TextMeshProUGUI priceDa;

    public GameObject life;
    public GameObject velocity;
    public GameObject damage;

    public static string maxValue = "Máximo";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinsGeneral.text = gameManager.TotalCoinsGeneral.ToString();
        recordMax.text = gameManager.MaxPoint.ToString();
        recordNow.text = gameManager.NowPoint.ToString();

        priceVe.text = priceV.ToString();
        priceLi.text = priceL.ToString();
        priceDa.text = priceD.ToString();
        
    }

    public void UpgradeDamage()
    {
        if (upgradeDLevel < 8)
        {
            if (GameManager.totalCoinsGeneral >= priceD)
            {
                if (GameManager.degradeLife < 50)
                {
                    GameManager.degradeLife += 5f;
                }
                GameManager.totalCoinsGeneral -= 2000;
            }
            else
            {
                Debug.Log("No te alcanza krnal");
            }
            upgradeDLevel += 1;
            priceD += (priceD * 30)/100;
        }
        else
        {
            Destroy(this.damage);
            Debug.Log("Nivel Máximo Daño");
        }

    }

    public void UpgradeVelocity()
    {
        if (upgradeVLevel < 12)
        {
            if (GameManager.totalCoinsGeneral >= priceV)
            {
                if (GameManager.speedPlayer < 10)
                {
                    GameManager.speedPlayer += 0.5f;
                }
                GameManager.totalCoinsGeneral -= 1500;
            }
            else
            {
                Debug.Log("No te alcanza krnal");
            }
            upgradeVLevel += 1;
            priceV += (priceV * 30)/100;
        }
        else
        {
            Destroy(this.velocity);
            Debug.Log("Nivel Máximo Velocidad");
        }
        

    }

    public void UpgradeLife()
    {
        if (upgradeLLevel < 5)
        {
            if (GameManager.totalCoinsGeneral >= priceL)
            {
                if (GameManager.lifes < 10)
                {
                    GameManager.lifes += 1;
                    GameManager.maxLifes += 1;
                }
                GameManager.totalCoinsGeneral -= 100000;
            }
            else
            {
                Debug.Log("No te alcanza krnal");
            }
            upgradeLLevel += 1;
            priceL += (priceL * 30)/100;
        }else
        {
            Destroy(this.life);
            Debug.Log("Nivel Máximo Vida");
        }


    }
}
