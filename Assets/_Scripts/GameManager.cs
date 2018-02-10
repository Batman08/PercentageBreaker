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
    [Header("Percentage Values")]
    //In the next update, change the way i calculate the percentage
    public float CurrentValue;
    public float MaxValue = 1.23f;
    public float finalValue;

    public float BufferPercent;
    [HideInInspector]
    public float MaxBufferValue = 1000.1f;

    private Stick _stick;
    private TargetValues _values;
    private float ChangePercentTime = 2;
    [HideInInspector]
    public float ChangeBufferPercentTime = 4;
    private float TextBuferNumber;

    void Awake()
    {
        FindComponents();
    }

    void FindComponents()
    {
        _stick = FindObjectOfType<Stick>();
        _values = FindObjectOfType<TargetValues>();
        MaxValue = 1.23f;
        Instantiate(BladePrefab);
        Manager = this;
        BufferPercent = MaxBufferValue;
        Debug.Log(BufferPercent);
    }

    void Start()
    {
        InvokeRepeating("ChangePercentage", 0, ChangePercentTime);
        InvokeRepeating("ChangeBufferPercentage", 0, ChangeBufferPercentTime);
        //InvokeChangesForPercentages();
    }

    void Update()
    {
        CalculatePercentage();
        PercentBufferText.text = "Buffer: " + TextBuferNumber;
    }

    void CalculatePercentage()
    {
        bool BufferIsAtMinimum = (BufferPercent <= 0.01f);
        if (BufferIsAtMinimum)
        {
            BufferPercent = 0.01f;
        }

        finalValue = (CurrentValue / MaxValue * 100);

        PercentText.text = "Percentage: " + Mathf.Round(finalValue/* + 10*/);
    }

    void ChangePercentage()
    {
        CurrentValue = Random.Range(0, MaxValue);
    }

    void ChangeBufferPercentage()
    {
        BufferPercent -= 0.1f;

    }
}
