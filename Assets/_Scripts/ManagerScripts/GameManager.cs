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
        StickSpawner.stickSpawner.SpawnStick();
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
        CalculatePercentageTextOutput();
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
            StartCoroutine(ChangePercentageAnim());
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
        float time = 1.35f;
        TextAnim.SetBool("HidePercentageText", true);
        yield return new WaitForSeconds(time);
        TextAnim.SetBool("HidePercentageText", false);
    }

    void UpdateLivesText()
    {
        LivesText.text = "Lives: " + _lives.Lives;
    }

    void StartSpawningSticks()
    {
        StickSpawner.stickSpawner.HasWaveEnded = false;
        ChangePercentage();
        StickSpawner.stickSpawner.SpawnStick();
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
        SceneManager.LoadScene(1);
    }
    #endregion

    public static int NumberOFDeaths = 0;
    private readonly string DeathCountKey = "DeathCount";

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
            _lives.gameObject.SetActive(value: false);
            //Destroy(_bladeCollisions.gameObject);
        }
        //Debug.Log(PlayerPrefs.GetInt(DeathCountKey));
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

    #region Not Used Methods
    void StopPercentageTextAnimation()
    {
        TextAnim.SetBool("HidePercentageText", true);
        TextAnim.SetBool("ShowNewPercentage", false);
    }
    #endregion
    #endregion
}
