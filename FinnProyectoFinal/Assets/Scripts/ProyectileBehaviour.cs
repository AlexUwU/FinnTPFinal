using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileBehaviour : MonoBehaviour
{
    public float speedProyectile = 10f;
    public Vector2 direction = Vector2.up;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speedProyectile * Time.deltaTime);
        Destroy(gameObject, 3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Grid"))
        {
            Destroy(this.gameObject);
        }
    }
}
