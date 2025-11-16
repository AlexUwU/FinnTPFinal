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

    //  AUDIO PARA PICKUPS (NUEVO)
    public AudioSource upgradeSound;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animation = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CreateProyectile();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (enableMove == false)
            return;

        //  velocidad final (upgrades + bonos)
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * GameManager.CurrentSpeed;

        animation.SetBool("isRun", (horizontal != 0f || vertical != 0f));

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CinemachineScreemShake.Instance.moveCamera(1, 1.2f, 0.15f);
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
        if ((viewRight && horizontal < 0) || (!viewRight && horizontal > 0))
        {
            viewRight = !viewRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void CreateProyectile()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.up;
            Instantiate(proyectile, transform.position, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.right;
            Instantiate(proyectile, transform.position, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.down;
            Instantiate(proyectile, transform.position, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            proyectile.GetComponent<ProyectileBehaviour>().direction = Vector2.left;
            Instantiate(proyectile, transform.position, transform.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            animation.SetBool("isDamage", true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            animation.SetBool("isDamage", false);
            CinemachineScreemShake.Instance.moveCamera(15, 8, 0.5f);
        }
    }
}
