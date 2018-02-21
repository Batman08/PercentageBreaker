using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public int Lives;

    private int _maxLives = 3;
    private bool DidCollideWithStick = false;
    private ScoreManager _scoreManager;
    private GameManager _manager;
    private CircleCollider2D _circleCollider2D;

    void Start()
    {
        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
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
        int BladeLayer = 11;
        int StickLayer = 8;

        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            //Debug.Log("You Win");
            //_circleCollider2D.enabled = false;
            //DidCollideWithStick = false;
            //IgnoreLayerCollisions();
            DestroyStick(collision);
            _scoreManager.AddScore();
            //   return;
            /*Need to find a way for the buffer to not collide with stick 
             * so when blade collides with the buffer it doesnt take a life away*/
        }

        else if (SlicedStick)
        {
            //   DidCollideWithStick = true;
            //if (DidCollideWithStick)
            //{

            //}if (Lives > 0)
            {
                Lives--;
            }
            //Debug.Log("Game Over");
        }
    }

    void DestroyStick(Collider2D collision)
    {
        Destroy(collision.transform.parent.gameObject);
    }
}
