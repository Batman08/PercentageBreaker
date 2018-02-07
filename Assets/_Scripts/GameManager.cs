using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager { get; set; }

    public GameObject BladePrefab;
    public Text PercentText;
    public float CurrentValue;
    public float MaxValue = 1.23f;
    public float finalValue;

    public float BufferPercent;
    [HideInInspector]
    public float MaxBufferValue = 0.9f;

    private Stick _stick;
    private float ChangePercentTime = 2;
    private float ChangeBufferPercentTime = 5;

    void Awake()
    {
        _stick = FindObjectOfType<Stick>();
        MaxValue = 1.23f;
        Instantiate(BladePrefab);
        StartCoroutine(ChangePercentage(ChangePercentTime));
        StartCoroutine(ChangeBufferPercentage(ChangeBufferPercentTime));
        Manager = this;
        BufferPercent = MaxBufferValue;
    }

    void Update()
    {
        bool BufferIsAtMinimum = (BufferPercent <= 0.01f);
        if (BufferIsAtMinimum)
        {
            BufferPercent = 0.01f;
        }

        finalValue = (CurrentValue / MaxValue * 100);

        PercentText.text = "Percentage: " + Mathf.Round(finalValue/* + 10*/);
    }

    IEnumerator ChangePercentage(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentValue = Random.Range(0, MaxValue);
    }

    IEnumerator ChangeBufferPercentage(float time)
    {
        yield return new WaitForSeconds(time);
        BufferPercent -= 0.01f;
    }
}
