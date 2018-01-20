using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageMathCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool destroyStick = (collision.collider.CompareTag("Target"));

        if (destroyStick)
        {
            Destroy(collision.gameObject);
        }
    }
}
