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

    void Start()
    {
        GoogleAdManager.Instance.ShowBannerAd();

        //Shows the user their high score
        string HighScore = PlayerPrefs.GetInt(HighscoreString).ToString();
        ScoreText.text = HighScore;

        CheckSoundButtonState();

        bool FirstTimeOn = (PlayerPrefs.GetFloat(SoundIsOnKey) == 0);
        if (FirstTimeOn)
        {
            IsSoundOn = true;
            PlayerPrefs.SetFloat(SoundIsOnKey, 1);
        }
    }

    void Update()
    {
        UpdateSoundButtonText();
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
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        //Handheld.Vibrate();
        Debug.Log("Quit Game!");
    }

    public void Sound()
    {
        //Check to see if the sound should be enabled or disabled
        if (IsSoundOn)
        {
            IsSoundOn = false;
            PlayerPrefs.SetInt(SoundKey, 0);
            //GoogleAdManager.Instance.ShowBannerAd();
        }
        else
        {
            IsSoundOn = true;
            PlayerPrefs.SetInt(SoundKey, 1);
        }
    }
}
