using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public AudioSource heal;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bool lifeRestart = GameManager.Instance.AddLife();
            if(lifeRestart)
            {
                heal.Play();
                Destroy(this.gameObject);
            }
        }
    }
}
