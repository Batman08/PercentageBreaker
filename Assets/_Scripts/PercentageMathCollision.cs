using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageMathCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            Debug.Log("You Win");
            DestroyStick(collision);
        }

        else if (SlicedBufferTarget && SlicedStick)
        {
            Debug.Log("Sliced both");
            DestroyStick(collision);
        }

        else
        {
            Debug.Log("Game Over");
        }
    }

    void DestroyStick(Collider2D collision)
    {
        Destroy(collision.transform.parent.gameObject);
    }
}
