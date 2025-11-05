
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HUD hud;
    public static int lifes = 5;
    public static int maxLifes = 5;
    public static GameManager Instance {get; private set;}

    public int TotalPoints {get {return totalPoints; }}
    public int TotalCoins {get {return totalCoins;} }
    public int TotalCoinsGeneral {get {return totalCoinsGeneral;} }
    public int MaxPoint {get {return maxPoint;} }
    public int NowPoint {get {return nowPoint;} }

    public static int maxPoint = 15000;
    public static int nowPoint = 0;
    public static int totalPoints;
    public static int totalCoins;
    public static int totalCoinsGeneral = 5000;
    public static float degradeLife = 10;
    public static float speedPlayer = 4f;

    public AudioClip damageProtagonist;

    
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DecreaseLife()
    {
        lifes -= 1;
        SoundManager.Instance.audioSource.PlayOneShot(damageProtagonist, 1f);
        if(lifes == 0)
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
        if(lifes == maxLifes)
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
        totalCoins += (totalPoints * 5)/100;
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
        if(totalPoints > nowPoint)
        {
            nowPoint = totalPoints;
        }
        else if (nowPoint > maxPoint)
        {
            maxPoint = totalPoints;
        }
    }

    // public void addMaxPoint()
    // {
    //     if(nowPoint > maxPoint)
    //     {
    //         maxPoint = nowPoint;
    //     }else
    //     {
    //         nowPoint = totalPoints;
    //     }
    // }
}
