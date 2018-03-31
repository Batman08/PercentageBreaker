using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStickMainMenu : MonoBehaviour
{
    private float _speed = 1f;

    void Update()
    {
        transform.Rotate(0, 0, _speed);

    }
}
