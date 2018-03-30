using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandCollision : MonoBehaviour
{
    public GameObject slicedStick;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stick"))
        {
            Destroy(collision.gameObject);
            Instantiate(slicedStick);
        }
    }
}
