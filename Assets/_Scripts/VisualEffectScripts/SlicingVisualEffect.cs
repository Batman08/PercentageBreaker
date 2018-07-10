using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicingVisualEffect : MonoBehaviour
{
    private GameObject _stick;

    void Awake()
    {
        _stick = GameObject.FindGameObjectWithTag("Stick");
    }

    void OnEnable()
    {
        transform.position = _stick.transform.position;
    }

}
