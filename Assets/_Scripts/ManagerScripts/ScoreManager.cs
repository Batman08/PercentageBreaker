using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text EndGameScoreText;

    [HideInInspector]
    public int ScoreCount;
    [HideInInspector]
    public int StickScoreValue = 1;
    private GameManager _manager;
    private string HighScoreKeyString = "Score";

    void Awake()
    {
        ScoreCount = 0;
        _manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateScore(ScoreCount);

        if (_manager._gameOver)
        {
            ShowEndGameScore(ScoreCount);
        }
    }

    //public void SaveHighScore()
    //{
    //    SaveLoadManager.SaveScore(this);
    //}

    public void AddScore()
    {
        ScoreCount += StickScoreValue;
    }

    void UpdateScore(int Score)
    {
        ScoreText.text = "Score: " + Score;
    }
    void ShowEndGameScore(int Score)
    {
        EndGameScoreText.text = " " + Score;
    }

    public void SaveScore()
    {
        bool ScoreIsGreaterThanHighScore = (PlayerPrefs.GetInt(HighScoreKeyString) < ScoreCount);
        if (ScoreIsGreaterThanHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKeyString, ScoreCount);
        }
    }
}
