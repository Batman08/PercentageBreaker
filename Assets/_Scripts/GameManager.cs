﻿using System.Collections;
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
    private float ChangePercentageTime = 25f;
    [HideInInspector]
    public float ChangeBufferPercentageTime = 35f;

    public float TextBufferNumber;

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
        UpdateTextBufferNumber();
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
        PercentBufferText.text = "Buffer: " + TextBufferNumber.ToString("F0") + "%";
    }

    void UpdateTextBufferNumber()
    {
        if (TextBufferNumber <= 5f)
            TextBufferNumber = 5;

        TextBufferNumber = BufferPercent * 10;
    }

    void FindComponents()
    {
        EndGamePanel.SetActive(value: false);
        // _spawner = FindObjectOfType<WaveSpawner>();
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = MaxBufferValue;
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

    void ChangeBufferPercentage()
    {
        float MinBufferPercentage = 0.5f;
        bool BufferIsNotAtMinimum = (BufferPercent > MinBufferPercentage);
        if (BufferIsNotAtMinimum)
        {
            BufferPercent -= 0.1f;
            //TextBufferNumber -= 1;
        }
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
