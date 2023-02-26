using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject hudCanvas;

    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused){
            PauseGame();
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused){
            ResumeGame();
        }
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        hudCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        hudCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        isPaused = false;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public bool GameIsPaused(){
        return isPaused;
    }
}
