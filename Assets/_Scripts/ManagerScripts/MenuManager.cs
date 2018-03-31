using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text ScoreText;
    public Text SoundButtonText;
    public bool IsSoundOn = true;

    private string HighscoreString = "Score";

    void Awake()
    {

        string HighScore = PlayerPrefs.GetInt(HighscoreString).ToString();
        ScoreText.text = HighScore;


    }

    void Update()
    {
        if (IsSoundOn)
            SoundButtonText.text = "ON";
        else
            SoundButtonText.text = "OFF";
    }

    public void PlayGameBtn()
    {
        SceneManager.LoadScene(1);
        // AdManager.Instance.ShowVideo();
    }

    public void QuitGame()
    {
        Application.Quit();
        //Handheld.Vibrate();
        Debug.Log("Quit Game!");
    }

    public void Sound()
    {
        //IsSoundOn = (IsSoundOn == false) ? IsSoundOn : !IsSoundOn;
        //backGroundAudioSource.mute = !backGroundAudioSource.mute;
        if (IsSoundOn)
            IsSoundOn = false;
        else
            IsSoundOn = true;
    }
}
