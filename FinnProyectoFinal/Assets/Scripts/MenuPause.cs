using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject menuPause;
    private bool menuOn;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuOn = !menuOn;
        }

        if(menuOn == true)
        {
            menuPause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }else
        {
            MenuPauseLock();
        }
    }

    public void Return()
    {
        MenuPauseLock();
        menuOn = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void MenuPauseLock()
    {
        menuPause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}