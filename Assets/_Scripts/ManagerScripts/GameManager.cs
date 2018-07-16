using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    #region
    public static GameManager Manager { get; set; }

    public bool ShowTextAnim = true;
    public bool _dontWantToSeeAnAd = true;

    public GameObject ContinueButton;

    public Animator TextAnimator;

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
    private LivesManager _lives;
    private ScoreManager _scoreManager;
    public CameraShake _cameraShake;

    [HideInInspector]
    public bool _gameOver = false;
    private float TextBuffer;
    public float textbufferValue = 70;
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
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = MaxBufferValue;
        ChangePercentage();
        ContinueButton.SetActive(value: false);
        _cameraShake = Camera.main.GetComponent<CameraShake>();

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        string SoundKey = "Value";
        bool SoundShouldBeON = (PlayerPrefs.GetInt(SoundKey) == 1);
        if (SoundShouldBeON)
            audioManager.gameObject.SetActive(value: true);
        else
            audioManager.gameObject.SetActive(value: false);
    }

    void Start()
    {
        //Changes percentage and buffer percentage
        //InvokeRepeating("ChangePercentage", 0, ChangePercentageTime);
        //InvokeRepeating("ChangeBufferPercentage", ChangeBufferPercentageTime, ChangeBufferPercentageTime);
        _lives = FindObjectOfType<LivesManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }
    #endregion

    #region Update Methods
    void Update()
    {
        //CalculatePercentage();
        //if (!StickSpawner.stickSpawner.HasWaveEnded)
        //{
        //    PlayPercentageTextAnimation();
        //}
        CalculatePercentageTextOutput();
        CheckIfWaveHasEnded();
        CheckIfBufferGoesOverMax();
        UpdateLivesText();
        CheckForAds();


        bool PastFiveRounds = (PlayerPrefs.GetInt(RoundCountKey) >= 2);
        if (PastFiveRounds)
        {
            ChangeValue = true;
            ChangeValue = false;
        }

        ChangeValues();
    }

    private readonly string RoundCountKey = "NumberOfRoundsKey";

    void CalculatePercentageTextOutput()
    {
        float FinalBufferValue;

        Percentage = NumberOutOfPercent;
        TextBuffer = Percentage + textbufferValue;

        FinalBufferValue = Mathf.RoundToInt(TextBuffer);
        PercentNumText1.text = Percentage + "%";
        PercentNumText2.text = Mathf.RoundToInt(FinalBufferValue) + "%";
        PercentText.text = "Cut Between " + "        " + " and";
    }

    void CheckIfBufferGoesOverMax()
    {
        bool BufferIsGreaterThanAHundred = (TextBuffer >= 100);
        if (BufferIsGreaterThanAHundred)
        {
            TextBuffer = 100;
        }
    }

    void CheckIfWaveHasEnded()
    {
        bool WaveHasEnded = (StickSpawner.stickSpawner.HasWaveEnded);
        if (WaveHasEnded)
        {
            //StartCoroutine(ChangePercentageAnim());
            StartSpawningSticks();
        }

    }

    void CheckForAds()
    {
        bool CanShowAdToContinueGame = (PlayerPrefs.GetInt(DeathCountKey) >= 2);
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
        float time = 1.05f;
        TextAnimator.SetBool("HidePercentageText", true);
        yield return new WaitForSeconds(time);
        TextAnimator.SetBool("HidePercentageText", false);
    }

    void UpdateLivesText()
    {
        LivesText.text = "Lives: " + _lives.Lives;
    }

    void StartSpawningSticks()
    {
        ChangePercentage();
        StickSpawner.stickSpawner.SpawnStick();
        //StickSpawner.stickSpawner.HasWaveEnded = false;
    }

    void ChangePercentage()
    {
        if (textbufferValue == 70)
        {
            NumberOutOfPercent = Random.Range(0, 30);
        }

        else if (textbufferValue >= 60)
        {
            NumberOutOfPercent = Random.Range(0, 40);
        }

        else if (textbufferValue >= 50)
        {
            NumberOutOfPercent = Random.Range(0, 50);
        }

        else if (textbufferValue >= 40)
        {
            NumberOutOfPercent = Random.Range(0, 60);
        }

        else if (textbufferValue >= 30)
        {
            NumberOutOfPercent = Random.Range(0, 70);
        }

        else if (textbufferValue >= 20)
        {
            NumberOutOfPercent = Random.Range(0, 80);
        }
        StartCoroutine(ChangeTextColour());

    }

    IEnumerator ChangeTextColour()
    {
        PercentNumText1.color = Color.yellow;
        PercentNumText2.color = Color.yellow;
        yield return new WaitForSeconds(5f);
        PercentNumText1.color = Color.red;
        PercentNumText2.color = Color.red;
    }

    void PlayPercentageTextAnimation()
    {
        TextAnimator.SetBool("ShowNewPercentage", true);

        //TextAnim.SetBool("HidePercentageText", false);
        // ShowTextAnim = false;
    }
    #endregion

    #region Button Methods
    public void RestartGame()
    {
        _dontWantToSeeAnAd = true;
        _scoreManager.CheckIfHighScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GoogleAdManager.Instance.ShowVideoAd();
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
    #endregion

    public static int NumberOFDeaths = 0;
    private readonly string DeathCountKey = "DeathCount";

    #region OtherMethods
    public void GameOver()
    {
        NumberOFDeaths += 1;
        _scoreManager.ShowScore = true;
        PlayerPrefs.SetInt(DeathCountKey, NumberOFDeaths);
        _gameOver = true;
        GamePanel.SetActive(value: false);
        EndGamePanel.SetActive(value: true);

        if (_gameOver && _dontWantToSeeAnAd)
        {
            _scoreManager.CheckIfHighScore();
            _lives.gameObject.SetActive(value: false);
            Blade blade = FindObjectOfType<Blade>();
            blade.gameObject.SetActive(value: false);
        }
        CameraShaker.Instance.ShakeOnce(4, 4, 0.1f, 1);
    }

    public void ShowAdButton()
    {
        Ads.Instance.ShowRewardBasedAd();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //_gameOver = false;
        //StickSpawner.stickSpawner.HasWaveEnded = true;
        ////Time.timeScale = 0;
        //GamePanel.SetActive(value: true);
        //EndGamePanel.SetActive(value: false);
        //StickSpawner.stickSpawner.gameObject.SetActive(value: true);
        //_bladeCollisions.Lives = 1;
        //_bladeCollisions.gameObject.SetActive(value: true);
        //Time.timeScale = 1;
    }
    #endregion

    public float Length = 0.608f;

    //public void ChangeBufferPercentage()
    //{
    //    ChangeValues();

    //    if (ChangeValue)
    //    {
    //        float NewLength = Length * 0.05f;
    //        Length += NewLength;
    //        Debug.Log(Length);
    //    }
    //    return;
    //}

    public bool ChangeValue = false;

    void ChangeValues()
    {
        if (ChangeValue)
        {
            BufferPercent -= 0.05f;
            textbufferValue -= 5;

            //float NewLength = Length * 0.05f;
            //Length += NewLength;
            //Debug.Log(Length);

            ChangeValue = false;
            return;
        }

    }
    #endregion
}
