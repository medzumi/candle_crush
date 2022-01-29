using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject PauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePaused();
        }
    }

    public void TogglePaused() {
        if(gamePaused){
            gamePaused = false;
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else {
            gamePaused = true;
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }
    }
}
