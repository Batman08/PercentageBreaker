using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject BladePrefab;

    void Start()
    {
        Instantiate(BladePrefab);
    }
}
