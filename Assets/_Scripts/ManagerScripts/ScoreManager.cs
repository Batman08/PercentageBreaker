using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;
    public Text EndGameScoreText;

    public bool ShowScore = false;

    public int ScoreCount;
    [HideInInspector]
    //public int StickScoreValue = 1;
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

        if (ShowScore)
        {
            ShowScore = false;
            ShowEndGameScore();
        }
    }

    public void AddScore(int StickValue)
    {
        ScoreCount += StickValue;
    }

    void UpdateScore(int Score)
    {
        ScoreText.text = "Score: " + Score;
    }

    IEnumerator AnimateScoreText()
    {
        EndGameScoreText.text = "0";
        int score = 0;

        yield return new WaitForSeconds(0.7f);

        while (score < ScoreCount)
        {
            score++;
            EndGameScoreText.text = score.ToString();

            float time = 0.05f;
            yield return new WaitForSeconds(time);
        }
    }

    public void ShowEndGameScore()
    {
        //EndGameScoreText.text = " " + Score;
        StartCoroutine(AnimateScoreText());
    }

    public void CheckIfHighScore()
    {
        //IF YOU WANT TO DELETE HIGHSCORE THEN CHANGE THE SIGN TO >
        bool isScoreGreaterThanHighScore = (PlayerPrefs.GetInt(HighScoreKeyString) < ScoreCount);
        if (isScoreGreaterThanHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKeyString, ScoreCount);
            SaveScores();
        }


    }

    public void SaveScores()
    {
        SaveLoadManager.SaveScore(this);
    }
}
