﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text ScoreText;

    private string HighscoreString = "Score";

    void Awake()
    {

        string HighScore = PlayerPrefs.GetInt(HighscoreString).ToString();
        ScoreText.text = HighScore;


    }

    void Start()
    {

        //GoogleAdManager.Instance.ShowAd();
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
        AdManager.Instance.ShowVideo();
    }
}
