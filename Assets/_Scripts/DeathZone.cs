using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D Col)
    {
        bool StickcollidedWithDeathZone = (Col.CompareTag("Stick"));

        if (StickcollidedWithDeathZone)
        {
            Destroy(Col.gameObject);
        }
    }
}
