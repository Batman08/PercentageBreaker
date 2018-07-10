using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadHighScoreAtMenu : MonoBehaviour
{
    public Text HighScoreText;

    private string HighScoreString = "Score";
    public int ScoreCount;

    void Start()
    {
        Load();
    }

    public void Load()
    {
        int score = SaveLoadManager.LoadScoreManager();

        ScoreCount = score;
        HighScoreText.text = ScoreCount.ToString();
    }
}
