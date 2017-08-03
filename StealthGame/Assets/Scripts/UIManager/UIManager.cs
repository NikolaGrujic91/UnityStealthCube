using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject mainCamera;
    public bool isPaused;
    public bool gameOver;
    public bool levelCompleted;

	void Start () 
    {
        isPaused = false;
        gameOver = false;
        levelCompleted = false;
        
	}
	

	void Update () 
    {
        if (gameOver)
            GameOver(gameOver);
        else if (levelCompleted)
            LevelCompleted(levelCompleted);
        else
        {
            if (isPaused)
                PauseGame(true);
            else
                PauseGame(false);
        }

        if (Input.GetButtonDown("Cancel"))
            SwitchPause();
	}

    void PauseGame(bool state)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if(state)
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = false;
            Time.timeScale = 0.0f; // Paused
        }
        else
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = true;
            Time.timeScale = 1.0f; // Unpaused
        }

        pausePanel.SetActive(state);
    }

    void GameOver(bool state)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (state)
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = false;
            Time.timeScale = 0.0f; //Paused
        }
        else
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = true;
            Time.timeScale = 1.0f; //Unpaused
        }

        gameOverPanel.SetActive(state);
    }

    void LevelCompleted(bool state)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (state)
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = false;
            Time.timeScale = 0.0f; // Paused
        }
        else
        {
            mainCamera.GetComponent<MouseOrbit>().enabled = true;
            Time.timeScale = 1.0f; // Unpaused
        }

        levelCompletedPanel.SetActive(state);
    }

    public void SwitchPause()
    {
        if (isPaused)
            isPaused = false;
        else
            isPaused = true;
    }

    public void QuitToMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Next()
    {
        Application.LoadLevel(Application.loadedLevel+1);
    }
}
