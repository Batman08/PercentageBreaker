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
    public float MaxBufferValue = 0.1f;

    private Stick _stick;

    void Awake()
    {
        _stick = FindObjectOfType<Stick>();
        MaxValue = 1.23f;
        Instantiate(BladePrefab);
        StartCoroutine(ChangePercentage(2));
        Manager = this;
        BufferPercent = MaxBufferValue;
        //Debug.Log(WidthValue);

        InvokeRepeating("ChangeBufferPercentage", 0f, 1f);
    }

    void Update()
    {
        Debug.Log(BufferPercent);

        //if (CurrentValue >= 1)
        //{
        //    CurrentValue = Random.Range(1f, CurrentValue);
        //}
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

    void ChangeBufferPercentage()
    {
        BufferPercent -= 0.01f;
    }
}
