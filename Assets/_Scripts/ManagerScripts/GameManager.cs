using System.Collections;
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
    public float MaxBufferValue = 1f;
    public float TextBufferNumber;
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

        PercentNumText1.enabled = false;
        PercentNumText2.enabled = false;
        ChangePercentage();
        PercentNumText1.enabled = true;
        PercentNumText2.enabled = true;
        PlayPercentageTextAnimation();
        StickSpawner.stickSpawner.SpawnStick();
    }

    void Start()
    {
        //Changes percentage and buffer percentage
        //InvokeRepeating("ChangePercentage", 0, ChangePercentageTime);
        InvokeRepeating("ChangeBufferPercentage", ChangeBufferPercentageTime, ChangeBufferPercentageTime);
        _bladeCollisions = FindObjectOfType<BladeCollisions>();
        _scoreManager = GetComponent<ScoreManager>();
    }
    #endregion

    #region Update Methods
    void Update()
    {
        //CalculatePercentage();
        bool WaveHasEnded = (StickSpawner.stickSpawner.HasWaveEnded);
        if (WaveHasEnded)
        {

            StartSpawningSticks();
        }

        bool BufferIsGreaterThanAHundred = (Buffer >= 100);
        if (BufferIsGreaterThanAHundred)
        {
            Buffer = 100;
        }
        CalculatePercentage();
        UpdateTextBufferNumber();
        UpdateLivesText();

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

    void UpdateTextBufferNumber()
    {
        if (TextBufferNumber <= 5f)
            TextBufferNumber = 5;

        TextBufferNumber = BufferPercent * 10;
    }


    private float Buffer;

    void CalculatePercentage()
    {
        //Percentage = (NumberOutOfPercent / MaxPercentage);
        float FinalBufferValue;
        Percentage = NumberOutOfPercent;
        Buffer = Percentage + BufferPercent * 10;

        bool BufferIsGreaterThanAHundred = (Buffer >= 100);
        if (BufferIsGreaterThanAHundred)
        {
            Buffer = 100;
        }

        FinalBufferValue = Mathf.RoundToInt(Buffer);
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
        if (ShowTextAnim)
        {
            TextAnim.SetBool("ShowNewPercentage", true);
            TextAnim.SetBool("HidePercentageText", false);
        }
    }
    #endregion

    #region Button Methods
    public void RestartGame()
    {
        _scoreManager.SaveScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region OtherMethods
    public void GameOver()
    {
        _gameOver = true;
        _scoreManager.SaveScore();
        GamePanel.SetActive(value: false);
        EndGamePanel.SetActive(value: true);
        Destroy(_bladeCollisions.gameObject);
    }
    #endregion


    #region Not Used Methods
    void StopPercentageTextAnimation()
    {
        TextAnim.SetBool("HidePercentageText", true);
        TextAnim.SetBool("ShowNewPercentage", false);
    }

    void ChangeBufferPercentage()
    {
        float MinBufferPercentage = 0.009f;
        bool BufferIsNotAtMinimum = (BufferPercent > MinBufferPercentage);
        if (BufferIsNotAtMinimum)
        {
            BufferPercent -= 0.009f;
            //TextBufferNumber -= 1;
        }
    }
    #endregion
    #endregion
}
