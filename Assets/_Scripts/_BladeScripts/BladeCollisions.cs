using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BladeCollisions : MonoBehaviour
{
    public GameObject Tick;
    public GameObject Cross;
    public Text PercentCutText;

    public int Lives;

    private readonly int _maxLives = 3;

    public int _sticksDestroyed;
    private int BladeLayer;
    private int StickLayer;
    private ScoreManager _scoreManager;
    private GameManager _manager;
    private GameObject Stick;
    private GameObject _objStick;
    private GameObject _objCross;
    private bool _isCrossEnabled = false;
    private CircleCollider2D _bladeCollider;
    AudioManager _audioManager;
    //private StickBreak _stickBreak;

    void Awake()
    {
        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
        BladeLayer = LayerMask.NameToLayer("Blade");
        StickLayer = LayerMask.NameToLayer("Stick");
        //v_stickBreak = FindObjectOfType<StickBreak>();
        //Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
        _bladeCollider = GetComponent<CircleCollider2D>();
        EnableLayerCollisions();

    }

    void Update()
    {
        Stick = GameObject.FindGameObjectWithTag("Stick");
        CheckAmountOfLives();
        CheckAmountOfCrosses();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void CheckAmountOfLives()
    {
        bool ZeroLifesLeft = (Lives <= 0);
        if (ZeroLifesLeft)
        {
            //_manager.GameOver();

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

    void EnableLayerCollisions()
    {
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layermaskValue = LinecastCutter.linecastCutter.layerMask.value;
        LinecastCutter.linecastCutter.LinecastCut(LinecastCutter.linecastCutter.mouseStart, LinecastCutter.linecastCutter.mouseEnd, layermaskValue);
        DestroyStickObj(collision);
    }

    void DestroyMe(GameObject Gameobj)
    {

        bool StickSlicedSoundIsEnabled = _audioManager != null;
        if (StickSlicedSoundIsEnabled)
        {
            string SlashSound = "StickSlicedSound";
            _audioManager.Play(SlashSound);
        }


        Destroy(Gameobj, 4f);
    }

    void DestroyStickObj(Collider2D collision)
    {
        bool SlicedBufferTarget = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        bool SlicedBufferTargetLeft = (collision.GetComponent<Collider2D>().CompareTag("BufferLeft"));
        bool SlicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");


        if (SlicedBufferTarget)
        {
            //float cutPercent = Random.Range(5, 10);
            //float finalValue = GameManager.Manager.Percentage - cutPercent;
            //Debug.Log(finalValue);
            //PercentCutText.text = finalValue.ToString();

            DestroyMe(stickObject);
            _manager.ShowTextAnim = false;
            Collider2D StickCollider = Stick.gameObject.GetComponent<Collider2D>();
            if (StickCollider != null)
            {
                StickCollider.enabled = false;
            }

            //Debug.Log("Sliced stick at right position");

            _objStick = Instantiate(Tick, collision.transform.position, Quaternion.identity);
            StartCoroutine(TakeAwayObject(_objStick));
            _sticksDestroyed++;

            _scoreManager.AddScore(1);
            return;
        }

        #region
        //else if (SlicedBufferTargetLeft)
        //{
        //    float cutPercent = Random.Range(0, 5);
        //    float finalValue = GameManager.Manager.Percentage - cutPercent;
        //    PercentCutText.text = finalValue.ToString();
        //    DestroyMe(stickObject);
        //    _manager.ShowTextAnim = false;
        //    Collider2D StickCollider = Stick.gameObject.GetComponent<Collider2D>();
        //    if (StickCollider != null)
        //    {
        //        StickCollider.enabled = false;
        //    }

        //    Debug.Log("Sliced stick at right position");

        //    _objStick = Instantiate(Tick, collision.transform.position, Quaternion.identity);
        //    StartCoroutine(TakeAwayObject(_objStick));
        //    _sticksDestroyed++;

        //    _scoreManager.AddScore();
        //}
        #endregion

        else if (SlicedStick)
        {
            Lives--;
            DestroyMe(stickObject);
            if (!_isCrossEnabled)
                _objCross = Instantiate(Cross, transform.position, Quaternion.identity);

            StartCoroutine(TakeAwayObject(_objCross));
            return;
        }

        if (SlicedStick)
        {
            int BladeLayer = 11;
            int StickLayer = 8;

            Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, true);
            _bladeCollider.enabled = false;
        }

        else
        {
            int BladeLayer = 11;
            int BufferLayer = 9;

            Physics2D.IgnoreLayerCollision(BladeLayer, BufferLayer, true);
            _bladeCollider.enabled = false;
        }

        StickSpawner.stickSpawner.HasWaveEnded = true;
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