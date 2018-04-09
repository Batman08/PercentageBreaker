﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    #region
    public static GameManager Manager { get; set; }

    public bool ShowTextAnim = true;
    public bool _dontWantToSeeAnAd = true;

    public GameObject ContinueButton;

    public Animator TextAnim;

    #region Object Variables
    public GameObject BladePrefab;
    public GameObject GamePanel;
    public GameObject EndGamePanel;
    #endregion

    [Space]

    #region Text Variabled
    [Header("Texts")]
    public Text PercentText;
    public Text PercentNumText1;
    public Text PercentNumText2;
    public Text LivesText;
    #endregion

    [Space]

    #region Percentage Variables
    [HideInInspector]
    [Header("Percentage variables")]
    public float Percentage;
    public float NumberOutOfPercent;
    #endregion

    #region Buffer Variables
    [Header("Buffer Variables")]
    public float BufferPercent;
    [HideInInspector]
    public float MaxBufferValue = 0.7f;
    #endregion

    #region Percentages Timings
    [Header("Change Percentages Times")]
    private float ChangePercentageTime = 25f;
    [HideInInspector]
    public float ChangeBufferPercentageTime = 35f;
    #endregion

    #region Private Variables
    private BladeCollisions _bladeCollisions;
    private ScoreManager _scoreManager;

    [HideInInspector]
    public bool _gameOver = false;
    #endregion
    #endregion

    //Methods
    #region 
    #region Starting Methods
    void Awake()
    {
        FindComponents();
        //AdManager.Instance.ShowVideo();
    }

    void FindComponents()
    {
        GamePanel.SetActive(value: true);
        EndGamePanel.SetActive(value: false);
        // _spawner = FindObjectOfType<WaveSpawner>();
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = MaxBufferValue;
        ChangePercentage();
        StickSpawner.stickSpawner.SpawnStick();
        ContinueButton.SetActive(value: false);
    }

    void Start()
    {
        //Changes percentage and buffer percentage
        //InvokeRepeating("ChangePercentage", 0, ChangePercentageTime);
        //InvokeRepeating("ChangeBufferPercentage", ChangeBufferPercentageTime, ChangeBufferPercentageTime);
        _bladeCollisions = FindObjectOfType<BladeCollisions>();
        _scoreManager = GetComponent<ScoreManager>();
    }
    #endregion

    #region Update Methods
    void Update()
    {
        //CalculatePercentage();
        PlayPercentageTextAnimation();
        CheckIfWaveHasEnded();
        CheckIfBufferGoesOverMax();
        CalculatePercentage();
        UpdateLivesText();
        CheckForAds();
    }

    void CheckIfWaveHasEnded()
    {
        bool WaveHasEnded = (StickSpawner.stickSpawner.HasWaveEnded);
        if (WaveHasEnded)
        {
            //ShowNewPercentageTextAnim();
            //if (_bladeCollisions._sticksDestroyed >= 1)
            //{
            //    TextAnim.SetBool("HidePercentageText", true);
            //}
            StartCoroutine(ChangePercentageAnim());
            StartSpawningSticks();
        }

    }

    void CheckIfBufferGoesOverMax()
    {

        bool BufferIsGreaterThanAHundred = (TextBuffer >= 100);
        if (BufferIsGreaterThanAHundred)
        {
            TextBuffer = 100;
        }
    }

    void CheckForAds()
    {
        bool CanShowAdToContinueGame = (PlayerPrefs.GetInt(DeathCountKey) >= 3);
        if (CanShowAdToContinueGame)
        {
            ContinueButton.SetActive(value: true);
            _dontWantToSeeAnAd = false;
            NumberOFDeaths = 0;
            PlayerPrefs.SetInt(DeathCountKey, 0);
        }

        else
        {
            _dontWantToSeeAnAd = true;
        }
    }

    IEnumerator ChangePercentageAnim()
    {
        float time = 1.35f;
        TextAnim.SetBool("HidePercentageText", true);
        yield return new WaitForSeconds(time);
        TextAnim.SetBool("HidePercentageText", false);
    }

    void UpdateLivesText()
    {
        LivesText.text = "Lives: " + _bladeCollisions.Lives;
    }

    void StartSpawningSticks()
    {
        StickSpawner.stickSpawner.HasWaveEnded = false;
        ChangePercentage();
        StickSpawner.stickSpawner.SpawnStick();
    }

    private float TextBuffer;

    void CalculatePercentage()
    {
        //Percentage = (NumberOutOfPercent / MaxPercentage);
        float FinalBufferValue;
        Percentage = NumberOutOfPercent;
        TextBuffer = Percentage + BufferPercent * 10;

        bool BufferIsGreaterThanAHundred = (TextBuffer >= 100);
        if (BufferIsGreaterThanAHundred)
        {
            TextBuffer = 100;
        }

        FinalBufferValue = Mathf.RoundToInt(TextBuffer);
        PercentNumText1.text = Percentage + "%";
        PercentNumText2.text = Mathf.RoundToInt(FinalBufferValue) + "%";
        PercentText.text = "Cut Between " + "        " + " and";
    }

    void ChangePercentage()
    {
        NumberOutOfPercent = Random.Range(0, 100);
        StartCoroutine(ChangeTextColour());

    }

    IEnumerator ChangeTextColour()
    {
        PercentNumText1.color = Color.yellow;
        PercentNumText2.color = Color.yellow;
        yield return new WaitForSeconds(5);
        PercentNumText1.color = Color.red;
        PercentNumText2.color = Color.red;
    }

    void PlayPercentageTextAnimation()
    {
        TextAnim.SetBool("ShowNewPercentage", true);

        //TextAnim.SetBool("HidePercentageText", false);
        // ShowTextAnim = false;
    }

    //void ShowNewPercentageTextAnim()
    //{
    //    TextAnim.SetBool("HidePercentageText", true);
    //    TextAnim.SetBool("HidePercentageText", false);
    //    ShowTextAnim = true;
    //}
    #endregion

    #region Button Methods
    public void RestartGame()
    {
        _dontWantToSeeAnAd = true;
        _scoreManager.SaveScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GoogleAdManager.Instance.ShowVideoAd();
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    public static int NumberOFDeaths = 0;
    private string DeathCountKey = "DeathCount";

    #region OtherMethods
    public void GameOver()
    {
        NumberOFDeaths += 1;

        PlayerPrefs.SetInt(DeathCountKey, NumberOFDeaths);
        _gameOver = true;
        GamePanel.SetActive(value: false);
        EndGamePanel.SetActive(value: true);

        if (_gameOver && _dontWantToSeeAnAd)
        {
            _scoreManager.SaveScore();
            _bladeCollisions.gameObject.SetActive(value: false);
            //Destroy(_bladeCollisions.gameObject);
        }
        Debug.Log(PlayerPrefs.GetInt(DeathCountKey));
    }

    public void ShowAdButton()
    {
        GoogleAdManager.Instance.ShowVideoAd();
        _gameOver = false;
        StickSpawner.stickSpawner.HasWaveEnded = true;
        //Time.timeScale = 0;
        GamePanel.SetActive(value: true);
        EndGamePanel.SetActive(value: false);
        StickSpawner.stickSpawner.gameObject.SetActive(value: true);
        _bladeCollisions.Lives = 1;
        _bladeCollisions.gameObject.SetActive(value: true);
        //Time.timeScale = 1;
    }
    #endregion

    public void ChangeBufferPercentage()
    {
        BufferPercent -= 0.05f;
    }

    #region Not Used Methods
    void StopPercentageTextAnimation()
    {
        TextAnim.SetBool("HidePercentageText", true);
        TextAnim.SetBool("ShowNewPercentage", false);
    }
    #endregion
    #endregion
}
