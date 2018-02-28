using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text EndGameScoreText;

    [HideInInspector]
    public int _scoreCount;
    [HideInInspector]
    public int StickScoreValue = 1;
    private GameManager _manager;
    private string HighScoreKeyString = "Score";

    void Awake()
    {
        _scoreCount = 0;
        _manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateScore(_scoreCount);

        if (_manager._gameOver)
        {
            ShowEndGameScore(_scoreCount);
        }
    }

    //public void SaveHighScore()
    //{
    //    SaveLoadManager.SaveScore(this);
    //}

    public void AddScore()
    {
        _scoreCount += StickScoreValue;
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
        bool ScoreIsGreaterThanHighScore = (PlayerPrefs.GetInt(HighScoreKeyString) < _scoreCount);
        if (ScoreIsGreaterThanHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKeyString, _scoreCount);
        }
    }
}
