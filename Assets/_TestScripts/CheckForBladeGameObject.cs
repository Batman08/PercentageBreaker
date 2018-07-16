using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForBladeGameObject : MonoBehaviour
{
    private GameObject _bladeGameObject;

    private string _bladeTag = "BladeChecker";

    private int BladeLayer, BladeCheckingLayer;

    void Start()
    {
        BladeLayer = 11;
        BladeCheckingLayer = 12;

        Physics2D.IgnoreLayerCollision(BladeCheckingLayer, BladeLayer, true);
    }

    void Update()
    {
        _bladeGameObject = GameObject.FindGameObjectWithTag(_bladeTag);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool hasCollidedWithBlade = (collision.collider.tag == _bladeTag);

        if (hasCollidedWithBlade)
        {
            _bladeGameObject.GetComponent<Collider2D>().enabled = false;
        }

        else
        {
            _bladeGameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
