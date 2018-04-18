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
    private string SoundKey = "Value";
    private string SoundIsOnKey = "On";
    private ToggleController _toggleController;

    void Start()
    {

        //Shows the user their high score
        string HighScore = PlayerPrefs.GetInt(HighscoreString).ToString();
        ScoreText.text = HighScore;

        //CheckSoundButtonState();

    }

    void Update()
    {
        //if (_toggleController.gameObject != null)
        //{
        //    _toggleController = FindObjectOfType<ToggleController>();
        //}

    }

    void CheckSoundButtonState()
    {
        //Makes the sound button not return to default bool {sound on -- True}
        bool SoundShouldBeON = (PlayerPrefs.GetInt(SoundKey) == 1);
        if (SoundShouldBeON)
            IsSoundOn = true;
        else
            IsSoundOn = false;
    }

    void UpdateSoundButtonText()
    {
        //Change button text
        if (IsSoundOn)
            SoundButtonText.text = "ON";
        else
            SoundButtonText.text = "OFF";
    }

    public void PlayGameBtn()
    {
        //Destroys the ad incase it continues to next scene
        //Admob.Instance().removeBanner();

        //Go to game scene
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
        //Handheld.Vibrate();
        Debug.Log("Quit Game!");
    }

}
