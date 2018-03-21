using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public GameObject Tick;
    public GameObject Cross;

    public int Lives;

    private int _maxLives = 4;
    private int _sticksDestroyed;
    private int BladeLayer;
    private int StickLayer;
    private ScoreManager _scoreManager;
    private GameManager _manager;
    private GameObject Stick;
    private GameObject _objStick;
    private GameObject _objCross;
    private bool _isCrossEnabled = false;
    private RotateStick _stick;
    //private StickBreak _stickBreak;

    void Start()
    {
        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
        BladeLayer = LayerMask.NameToLayer("Blade");
        StickLayer = LayerMask.NameToLayer("Stick");
        //v_stickBreak = FindObjectOfType<StickBreak>();
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
        _stick = FindObjectOfType<RotateStick>();
    }

    void Update()
    {
        Stick = GameObject.FindGameObjectWithTag("Stick");
        CheckAmountOfLives();
        CheckAmountOfCrosses();
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

    void CheckAmountOfCrosses()
    {
        if (_objCross != null)
            _isCrossEnabled = true;

        else
            _isCrossEnabled = false;
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
        DestroyStickObj(collision);
    }

    void DestroyStickObj(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));

        if (SlicedBufferTarget)
        {
            _stick.DestroyMe();
            _manager.ShowTextAnim = false;
            Collider2D StickCollider = Stick.gameObject.GetComponent<Collider2D>();
            if (StickCollider != null)
            {
                StickCollider.enabled = false;
            }

            //if (Input.GetMouseButtonUp(0))
            //{
            //    int layermaskValue = LinecastCutter.linecastCutter.layerMask.value;
            //    LinecastCutter.linecastCutter.LinecastCut(LinecastCutter.linecastCutter.mouseStart, LinecastCutter.linecastCutter.mouseEnd, layermaskValue);
            //}

            Debug.Log("Sliced stick at right position");
            //DestroyStick(collision, 10f);

            _objStick = Instantiate(Tick, collision.transform.position, Quaternion.identity);
            StartCoroutine(TakeAwayObject(_objStick));
            //  LinecastCutter.linecastCutter.LinecastCut(LinecastCutter.linecastCutter.mouseStart, LinecastCutter.linecastCutter.mouseEnd, LinecastCutter.linecastCutter.layerMask.value);
            _sticksDestroyed++;
            //StickSpawner.stickSpawner.HasWaveEnded = true;
            _scoreManager.AddScore();
            //   return;
        }

        else if (SlicedStick)
        {
            _stick.DestroyMe();
            if (!_isCrossEnabled)
                _objCross = Instantiate(Cross, transform.position, Quaternion.identity);

            StartCoroutine(TakeAwayObject(_objCross));
            //return;
        }
    }


    void DestroyStick(Collider2D collision, float time)
    {
        Destroy(collision.transform.parent.gameObject, time);
    }

    IEnumerator TakeAwayObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        //Destroy(obj.gameObject);
    }
}
