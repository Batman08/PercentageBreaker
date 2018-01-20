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
    public float WidthValue;
    public float finalValue;
    public float errorZone = 10f;

    private Stick _stick;

    void Start()
    {
        _stick = FindObjectOfType<Stick>();
        WidthValue = _stick.spriterenderer.bounds.size.x;
        Instantiate(BladePrefab);
        StartCoroutine(ChangePercentage(1));
        Manager = this;
    }

    void Update()
    {
        //if (CurrentValue >= 1)
        //{
        //    CurrentValue = Random.Range(1f, CurrentValue);
        //}

        finalValue = (CurrentValue / WidthValue * 100);

        PercentText.text = "Percentage: " + Mathf.Round(finalValue/* + 10*/);
    }

    IEnumerator ChangePercentage(float time)
    {
        yield return new WaitForSeconds(time);
        CurrentValue = Random.Range(0, WidthValue);
    }
}
