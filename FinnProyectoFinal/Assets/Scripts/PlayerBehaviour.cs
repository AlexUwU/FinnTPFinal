using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private bool viewRight = true;
    private Animator animation;
    public GameObject proyectile;
    public float strengPunch;
    public Rigidbody2D rigidBody;
    public bool enableMove = true;
    private BoxCollider2D boxCollider;
    public float horizontal;
    public float vertical;

    public AudioClip shootBall;
    private AudioSource audioSource;
    // public LayerMask capaSuelo;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloquea el cursor para que no salga de pantalla
        Cursor.visible = false; //Pone invisible al cursor del mouse
        animation = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CreateProyectile();
    }

    // bool OnWall()
    // {
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
    //     return raycastHit.collider != null;
    // }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(enableMove == false)
        {
            return;
        }
        // transform.Translate(new Vector2(horizontal, vertical).normalized * speedPlayer * Time.deltaTime); //Permite mover al jugador
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * GameManager.speedPlayer;
        if(horizontal != 0f || vertical != 0f)
        {
            animation.SetBool("isRun", true);
        }
        else
        {
            animation.SetBool("isRun", false);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CinemachineScreemShake.Instance.moveCamera(1,1.2f,0.15f);
            animation.SetBool("isAttack", true);
        }
        else
        {
            animation.SetBool("isAttack", false);
        }
        OnDirection(horizontal);
    }

    void OnDirection(float horizontal)
    {
        if((viewRight == true && horizontal < 0) || (viewRight == false && horizontal > 0))
        {
            viewRight = !viewRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void CreateProyectile()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.up;
            Instantiate(proyectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(shootBall, 0.6f);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.right;
            Instantiate(proyectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(shootBall, 0.6f);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.down;
            Instantiate(proyectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(shootBall, 0.6f);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.left;
            Instantiate(proyectile, transform.position, transform.rotation);
            audioSource.PlayOneShot(shootBall, 0.6f);
        }
    }

    // public void AddPunch()
    // {
    //     enableMove = false;
    //     Vector2 directionPunch;
    //     if(rigidBody.velocity.x > 0)
    //     {
    //         directionPunch = new Vector2(-1, 1);
    //     }
    //     else
    //     {
    //         directionPunch = new Vector2(1, 1);
    //     }
    //     rigidBody.AddForce(directionPunch * strengPunch);
    //     StartCoroutine(WaitingMove());
    // }

    // IEnumerator WaitingMove()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     while(!OnWall())
    //     {
    //         yield return null;
    //     }
    //     enableMove = true;
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            animation.SetBool("isDamage", true);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            animation.SetBool("isDamage", false);
            CinemachineScreemShake.Instance.moveCamera(15,8,0.5f);
        }
    }
}
