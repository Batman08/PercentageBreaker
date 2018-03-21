using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject PauseButton;

    private GameManager _manager;

    void Awake()
    {
        GameIsPaused = false;
        PauseButton.SetActive(value: true);
        PauseMenuUI.SetActive(value: false);
        _manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        bool GamIsNotOver = (!GameIsPaused && _manager._gameOver == false);
        if (GamIsNotOver)
        {
            PauseButton.SetActive(value: true);
        }

        else
        {
            PauseButton.SetActive(value: false);
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(value: false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(value: true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
