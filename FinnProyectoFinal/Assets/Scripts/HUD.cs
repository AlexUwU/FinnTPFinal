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

    // Start is called before the first frame update
    void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        point.text = gameManager.TotalPoints.ToString();
        coins.text = gameManager.TotalCoins.ToString();
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
