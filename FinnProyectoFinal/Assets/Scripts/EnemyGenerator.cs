using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //public static EnemyGenerator Instance;
    public GameObject enemyPrefab;
    public float spacingX = 1.5f;
    public float spacingY = 1.5f;
    public int formationWidth = 3;
    public int formationHeight = 3;

    private int[,] formation;

    public Transform[] positionEnemy;

    private int randomPositionIndex;
    private Transform randomPosition;

    public int limitEnemy;
    public int limitEnemyTotal=200;
    private int contEnemy = 1;
    private float randomTime;
    public int tipeGenerator;

    public float constTime;
    public float initialTime;

    public int ContEnemy { get => contEnemy; set => contEnemy = value; }

    private void Awake() {
        // if(Instance == null){
        //     Instance=this;
        // }else{
        //     Destroy(gameObject);
        // }
    }

    void Start()
    {
        formation = new int[formationHeight, formationWidth];
        switch(tipeGenerator)
        {
            case 1:
            {
                Invoke("GenerateEnemies", initialTime); //Invoca inicialmente al enemigo
                break;
            }
        }
    }

    void GenerateFormation()
    {
        for (int row = 0; row < formationHeight; row++)
        {
            for (int column = 0; column < formationWidth; column++)
            {
                formation[row, column] = Random.Range(0, 2); // Genera valores aleatorios de 0 o 1 en la matriz
            }
        }
    }

    void GenerateEnemies()
    {
        GenerateFormation();
        randomPositionIndex = Random.Range(0, positionEnemy.Length);
        randomPosition = positionEnemy[randomPositionIndex];
        for (int row = 0; row < formationHeight; row++)
        {
            for (int column = 0; column < formationWidth; column++)
            {
                if (formation[row, column] == 1)
                {
                    
                    
                    if(contEnemy < limitEnemy)
                    {
                        Vector3 position = new Vector3(column * spacingX, row * spacingY, 0f);
                        Instantiate(enemyPrefab, randomPosition.position + position, Quaternion.identity);
                        
                        // Debug.Log(contEnemy);
                        contEnemy++; //Aumenta en 1 el contador de enemigo
                    }
                }
            }
        }
        if(limitEnemy < limitEnemyTotal)//en cada ronda se incrementa el limite en 5
        {
            limitEnemy += 5;
        }
        Invoke("GenerateEnemies", constTime); //Invoca al enemigo cada cierto tiempo constante (rondas de 15s)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GenerateEnemies();
        }
    }
}
