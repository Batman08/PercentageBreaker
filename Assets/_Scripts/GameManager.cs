using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BladePrefab;
    public float Percent;

    private Stick _stick;

    void Start()
    {
        if (_stick != null)
        {
            _stick = FindObjectOfType<Stick>();
        }
        Percent = _stick.spriterenderer.bounds.size.x;
        Instantiate(BladePrefab);
        Percent = Random.Range(0, Percent);
        Debug.Log(Percent/* * 100*/);
    }
}
