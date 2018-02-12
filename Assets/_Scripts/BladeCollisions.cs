using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public int Lives;
    private int _maxLives = 3;

    void Start()
    {
        Lives = _maxLives;
    }

    void Update()
    {
        CheckAmountOfLives();
    }

    void CheckAmountOfLives()
    {
        bool ZeroLifesLeft = (Lives <= 0);
        if (ZeroLifesLeft)
        {
            Debug.Log("GAME OVER");
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            //Debug.Log("You Win");
            DestroyStick(collision);
        }

        else if (SlicedStick)
        {
            Lives--;
            //Debug.Log("Game Over");
        }
    }

    void DestroyStick(Collider2D collision)
    {
        Destroy(collision.transform.parent.gameObject);
    }
}
