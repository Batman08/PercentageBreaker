using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        bool collidedWithBlade = (collision.CompareTag("Blade"));
        bool collidedWithDeathZone = (collision.CompareTag("DeathZone"));

        if (collidedWithBlade)
        {
            //ToDo: Instaniate a stick break effect
            Destroy(gameObject);
        }

        else if (collidedWithDeathZone)
        {
            Destroy(gameObject);
        }
    }
}
