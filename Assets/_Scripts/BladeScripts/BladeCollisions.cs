using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public int Lives;

    private int _maxLives = 4;
    private int _sticksDestroyed;
    private int BladeLayer;
    private int StickLayer;
    private ScoreManager _scoreManager;
    private GameManager _manager;

    void Start()
    {
        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
        BladeLayer = LayerMask.NameToLayer("Blade");
        StickLayer = LayerMask.NameToLayer("Stick");
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
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

        float MaxSticksDestroyedToGetAnotherLife = 10;
        bool CanGiveAnotherLife = (_sticksDestroyed >= MaxSticksDestroyedToGetAnotherLife);
        if (CanGiveAnotherLife)
        {
            Lives++;
            _sticksDestroyed = 0;
        }
    }

    void IgnoreLayerCollisions()
    {
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, true);
    }

    void EnableLayerCollisions()
    {
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            DestroyStick(collision);
            _sticksDestroyed++;
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
