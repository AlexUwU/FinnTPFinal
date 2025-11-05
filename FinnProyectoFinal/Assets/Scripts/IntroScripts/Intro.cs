using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public bool onIntro = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || onIntro == false)
        {
            SkipIntro();
        }
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("StartGame");
    }
}
