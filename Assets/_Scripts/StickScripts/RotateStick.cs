using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStick : MonoBehaviour
{
    private int _randomRotNum;

    //0.379125


    void OnEnable()
    {
        _randomRotNum = Random.Range(0, 360);
        transform.localRotation = Quaternion.Euler(0, 0, _randomRotNum);
    }

    void Update()
    {
        CheckForChildren();
    }

    public void DestroyMe()
    {
        Destroy(gameObject, 2f);
    }

    void CheckForChildren()
    {
        bool NoChildren = (transform.childCount <= 0);
        if (NoChildren)
        {
            Destroy(gameObject);
        }
    }
}
