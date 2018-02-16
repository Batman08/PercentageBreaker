using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; set; }

    public GameObject BladePrefab;
    public GameObject EndGamePanel;
    [Space]
    [Header("Texts")]
    public Text PercentText;
    public Text PercentBufferText;
    public Text LivesText;
    [Space]
    [HideInInspector]
    [Header("Percentage variables")]
    public float Percentage;
    public float NumberOutOfPercent;

    [Header("Buffer Variables")]
    public float BufferPercent;
    [HideInInspector]
    public float MaxBufferValue = 1f;

    [Header("Change Percentages Times")]
    public float ChangePercentageTime = 25;
    public float ChangeBufferPercentageTime = 60;

    private float TextBufferNumber = 11;

    private Stick _stick;
    private TargetValues _values;
    private BladeCollisions _bladeCollisions;

    [HideInInspector]
    public bool _gameOver = false;
    //private WaveSpawner _spawner;

    void Awake()
    {
        FindComponents();
    }

    void Start()
    {
        //Changes percentage and buffer percentage
        InvokeRepeating("ChangePercentage", 0, ChangePercentageTime);
        InvokeRepeating("ChangeBufferPercentage", ChangeBufferPercentageTime, ChangeBufferPercentageTime);
        _bladeCollisions = FindObjectOfType<BladeCollisions>();
        //InvokeRepeating("ChangeBufferPercentage", 0, ChangeBufferPercentTime);
    }

    void Update()
    {
        //CalculatePercentage();
        CalculatePercentage();
        KeepBufferPercentAtMinimum();
        UpdateBufferText();
        UpdateLivesText();

        #region
        //bool ChangePercentage = _spawner.changePercentage;
        //if (ChangePercentage)
        //{
        //    ChangePercentageTwo();
        //    ChangeBufferPercentage();
        //    _spawner.changePercentage = false;
        //}
        //if (BufferPercent <= 0.5f)
        //{
        //    ChangeBufferPercentTime = 60;
        //}
        #endregion
    }

    void UpdateBufferText()
    {
        PercentBufferText.text = "Buffer: " + TextBufferNumber + "%";

        if (TextBufferNumber <= 0)
            TextBufferNumber = 0;
    }

    void FindComponents()
    {
        EndGamePanel.SetActive(value: false);
        _stick = FindObjectOfType<Stick>();
        _values = FindObjectOfType<TargetValues>();

        // _spawner = FindObjectOfType<WaveSpawner>();
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = 1.1f; ;
        //Debug.Log(BufferPercent);
    }

    void CalculatePercentage()
    {
        //Percentage = (NumberOutOfPercent / MaxPercentage);
        Percentage = NumberOutOfPercent;
        PercentText.text = "Percentage: " + Percentage + "%";
    }

    void ChangePercentage()
    {
        NumberOutOfPercent = Random.Range(0, 100);
    }

    void KeepBufferPercentAtMinimum()
    {
        float MinBufferPercentage = 0.01f;
        bool BufferIsAtMinimum = (BufferPercent <= MinBufferPercentage);
        if (BufferIsAtMinimum)
        {
            BufferPercent = 0.03f;
        }
    }

    void ChangeBufferPercentage()
    {
        BufferPercent -= 0.1f;
        TextBufferNumber -= 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        _gameOver = true;
        EndGamePanel.SetActive(value: true);
    }

    void UpdateLivesText()
    {
        LivesText.text = "Lives: " + _bladeCollisions.Lives;
    }
}
