using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public int openSide;

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if(spawned == false)
        {
            if (openSide == 1)
            {
                rand = Random.Range(0, templates.bottonDungeon.Length);
                Instantiate(templates.bottonDungeon[rand], transform.position, templates.bottonDungeon[rand].transform.rotation);
            }
            else if (openSide == 2)
            {
                rand = Random.Range(0, templates.topDungeon.Length);
                Instantiate(templates.topDungeon[rand], transform.position, templates.topDungeon[rand].transform.rotation);
            }
            else if (openSide == 3)
            {
                rand = Random.Range(0, templates.leftDungeon.Length);
                Instantiate(templates.leftDungeon[rand], transform.position, templates.leftDungeon[rand].transform.rotation);
            }
            else if(openSide == 4)
            {
                rand = Random.Range(0, templates.rightDungeon.Length);
                Instantiate(templates.rightDungeon[rand], transform.position, templates.rightDungeon[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
