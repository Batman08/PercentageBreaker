using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; set; }

    public GameObject BladePrefab;
    [Space]
    [Header("Texts")]
    public Text PercentText;
    public Text PercentBufferText;
    [Space]
    [HideInInspector]
    [Header("Percentage variables")]
    public float Percentage;
    private float NumberOutOfPercent;
    private float ChangePercentageTime = 2;

    [Header("Buffer Variables")]
    public float BufferPercent;
    [HideInInspector]
    public float MaxBufferValue = 1.0f;
    [HideInInspector]
    public float ChangeBufferPercentTime = 4;
    private float TextBufferNumber = 11;

    private Stick _stick;
    private TargetValues _values;

    #region Completed Methods(That work)
    void Awake()
    {
        FindComponents();
    }

    void Start()
    {
        //Changes percentage and buffer percentage
        InvokeRepeating("ChangePercentageTwo", 0, ChangePercentageTime);
        InvokeRepeating("ChangeBufferPercentage", 0, ChangeBufferPercentTime);
    }

    void Update()
    {
        //CalculatePercentage();
        CalculatePercentageTwo();
        KeepBufferPercentAtMinimum();
        UpdateBufferText();
    }

    void UpdateBufferText()
    {
        PercentBufferText.text = "Buffer: " + TextBufferNumber;

        if (TextBufferNumber <= 0)
            TextBufferNumber = 0;
    }

    void FindComponents()
    {
        _stick = FindObjectOfType<Stick>();
        _values = FindObjectOfType<TargetValues>();
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = MaxBufferValue;
        Debug.Log(BufferPercent);
    }

    void CalculatePercentageTwo()
    {
        //Percentage = (NumberOutOfPercent / MaxPercentage);
        Percentage = NumberOutOfPercent;
        PercentText.text = "Percentage: " + Percentage;
    }

    void ChangePercentageTwo()
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
    #endregion




}
