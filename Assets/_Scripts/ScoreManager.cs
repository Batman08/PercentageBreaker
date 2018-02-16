using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text EndGameScoreText;

    private int _scoreCount;
    [HideInInspector]
    public int StickScoreValue = 1;
    private GameManager _manager;

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
}
