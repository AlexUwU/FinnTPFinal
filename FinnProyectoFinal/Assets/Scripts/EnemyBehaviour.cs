
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float rangeAttack = 10;
    [SerializeField] private float rangeAlert = 18;
    [SerializeField] private GameObject sprite;
    public GameObject enemyGenerator;

    private float speed;
    private int state;

    private Vector2 knockbackDirection;
    private bool isKnockback = false;
    private float knockbackTimer = 0f;
    private float knockbackForce = 1.5f;
    private float knockbackDuration = 0.6f;

    private Animator anim;

    public Image lifeBar;
    public float lifeMax;
    public float lifeNow;

    public int valor = 100;

    public AudioClip enemyDead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = StateMachine.INACTIVE;
        anim= GetComponent<Animator>();
        speed=minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = lifeNow/lifeMax;
        // Debug.Log(CalcDistance());

        if (CalcDistance() < rangeAlert)
        {
            anim.SetBool("isWalking", true);
            switch (state)
            {
                case StateMachine.INACTIVE:
                    // Debug.Log("Inactivo");
                    state = StateMachine.ALERT;
                    break;
                case StateMachine.ALERT:
                    // Debug.Log("Alert");
                    speed = minSpeed;
                    
                    MoveTowardsPlayer();
                    if (CalcDistance() < rangeAttack)
                    {
                        state = StateMachine.ATTACK;
                    }
                    break;
                case StateMachine.ATTACK:
                    // Debug.Log("attack");
                    speed = maxSpeed;
                    MoveTowardsPlayer();
                    break;
                default:
                    break;
            }
        }else
        {
            state = StateMachine.INACTIVE;
            anim.SetBool("isWalking", false);
        }

        if (isKnockback)
        {
            KnockbackMovement();  
            state = StateMachine.ATTACK;
        }
    }

    private void MoveTowardsPlayer() 
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calcular la función de una recta (y = mx) donde 'm' es la pendiente
        float m = direction.y / direction.x;

        // Calcular el desplazamiento en x y en y basado en la velocidad
        float dx = Mathf.Sqrt((speed * speed) / (1 + m * m));
        float dy = m * dx;

        // Invertir los desplazamientos si el jugador está a la izquierda del enemigo
        if (direction.x < 0)
        {
            dx = -dx;
            dy = -dy;
            sprite.GetComponent<SpriteRenderer>().flipX=true;
        }else{
            sprite.GetComponent<SpriteRenderer>().flipX=false;
        }

        // Calcular la nueva posición del enemigo
        Vector3 newPosition = transform.position + new Vector3(dx, dy, 0f) * Time.deltaTime;

        // Actualizar la posición del enemigo
        transform.position = newPosition;
    }

    private void KnockbackMovement()
    {
        knockbackTimer -= Time.deltaTime;
        if (knockbackTimer <= 0f)
        {
            isKnockback = false;
        }
        else
        {
            transform.position += (Vector3)knockbackDirection * knockbackForce * Time.deltaTime;
        }
    }

    public void StartKnockback(Vector2 direction)
    {
        isKnockback = true;
        knockbackTimer = knockbackDuration;
        knockbackDirection = direction.normalized;
    }

    public float CalcDistance() //Pitágoras
    {
        float distanceX = player.transform.position.x - transform.position.x;
        float distanceY = player.transform.position.y - transform.position.y;
        float distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
        return distance;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("ProyectilPlayer"))
        {
            lifeNow -= GameManager.degradeLife;
            StartKnockback(other.gameObject.GetComponent<ProyectileBehaviour>().direction);
            anim.SetTrigger("getHit");
            if(lifeNow <= 0)
            {
                SoundManager.Instance.audioSource.PlayOneShot(enemyDead, 1f);
                GameManager.Instance.AddPoint(valor);
                enemyGenerator.GetComponent<EnemyGenerator>().ContEnemy-=1;
                CinemachineScreemShake.Instance.moveCamera(7,5,0.1f);
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.DecreaseLife();
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            anim.SetBool("isAttacking", false);
        }
    }

    
}
