using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public int Lives;

    private int _maxLives = 4;
    private ScoreManager _scoreManager;
    private GameManager _manager;

    void Start()
    {
        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
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
            _manager.GameOver();

            return;
        }
    }

    void IgnoreLayerCollisions()
    {
        int BladeLayer = LayerMask.NameToLayer("Blade");
        int StickLayer = LayerMask.NameToLayer("Stick");

        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, true);
    }

    void EnableLayerCollisions()
    {
        int BladeLayer = LayerMask.NameToLayer("Blade");
        int StickLayer = LayerMask.NameToLayer("Stick");

        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            DestroyStick(collision);
            _scoreManager.AddScore();
        }

        else if (SlicedStick)
        {
            if (Lives > 0)
            {
                Lives--;
                StartCoroutine(StopBladeCollidingWithStick());
            }
        }
    }

    void DestroyStick(Collider2D collision)
    {
        Destroy(collision.transform.parent.gameObject);
    }

    IEnumerator StopBladeCollidingWithStick()
    {
        float time = 8f;
        IgnoreLayerCollisions();
        yield return new WaitForSeconds(time);
        EnableLayerCollisions();

    }
}
