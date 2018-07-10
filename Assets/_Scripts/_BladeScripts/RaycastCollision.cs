using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollision : MonoBehaviour
{
    public GameObject BufferCollisionPoint, BufferCollisionPoint2;
    public GameObject BufferCollisionPoint3, BufferCollisionPoint4;
    public GameObject start1, end1;
    public GameObject start2, end2;
    public LayerMask LayerMask;
    public LayerMask BufferLayerMask;

    public Vector2 BufferCollisionPointPos, BufferCollisionPointPos2;
    public Vector2 BufferCollisionPointPos3, BufferCollisionPointPos4;
    public Vector2 _point1, _point2;
    public Vector2 _point3, _point4;






    public GameObject Tick;
    public GameObject Cross;

    public int Lives;

    private readonly int _maxLives = 3;

    public int _sticksDestroyed;
    private int _bladeLayer;
    private int _stickLayer;
    private ScoreManager _scoreManager;
    private GameManager _manager;
    private GameObject _stick;
    private GameObject _objStick;
    private GameObject _objCross;
    private bool _isCrossEnabled = false;
    private CircleCollider2D _bladeCollider;






    void OnEnable()
    {
        SetRayCastingPosition();

        Lives = _maxLives;
        _scoreManager = FindObjectOfType<ScoreManager>();
        _manager = FindObjectOfType<GameManager>();
        _bladeLayer = LayerMask.NameToLayer("Blade");
        _stickLayer = LayerMask.NameToLayer("Stick");
        //v_stickBreak = FindObjectOfType<StickBreak>();
        Physics2D.IgnoreLayerCollision(_bladeLayer, _stickLayer, false);
        _bladeCollider = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        _stick = GameObject.FindGameObjectWithTag("Stick");
        //RaycastToSeeIfCollidingWithStick();
        RayCastCollisions();
    }

    void SetRayCastingPosition()
    {
        //BufferCollisionPoint.gameObject.transform.position = BufferCollisionPointPos;
        //BufferCollisionPoint2.gameObject.transform.position = BufferCollisionPointPos2;

        //BufferCollisionPoint3.gameObject.transform.position = BufferCollisionPointPos3;
        //BufferCollisionPoint4.gameObject.transform.position = BufferCollisionPointPos4;

        start1.transform.position = _point1;
        end1.transform.position = _point2;

        start2.transform.position = _point3;
        end2.transform.position = _point4;
    }

    public void RaycastToSeeIfCollidingWithStick()
    {
        GameObject stickGo = GameObject.FindGameObjectWithTag("Stick");

        Debug.DrawLine(start1.transform.position, end1.transform.position, Color.cyan);
        Debug.DrawLine(start2.transform.position, end2.transform.position, Color.green);

        bool CollidingWithStick1 = Physics2D.Linecast(start1.transform.position, end1.transform.position, LayerMask);
        bool CollidingWithStick2 = Physics2D.Linecast(start2.transform.position, end2.transform.position, LayerMask);
        bool StickIsEnabled = stickGo != null;

        if (CollidingWithStick1 && StickIsEnabled || CollidingWithStick2 && StickIsEnabled)
        {
            stickGo.GetComponent<Collider2D>().enabled = false;
        }

        else if (!CollidingWithStick1/* && !CollidingWithStick2 */&& StickIsEnabled)
        {
            stickGo.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void RayCastCollisions()
    {
        CheckForCollisions();
    }

    void CheckForCollisions()
    {

        int layermaskValue = LinecastCutter.linecastCutter.layerMask.value;
        LinecastCutter.linecastCutter.LinecastCut(LinecastCutter.linecastCutter.mouseStart, LinecastCutter.linecastCutter.mouseEnd, layermaskValue);
        Debug.DrawLine(BufferCollisionPoint.transform.position, BufferCollisionPoint2.gameObject.transform.position, Color.blue);

        CollidingWithBuffer();
        CollidingWithStick();
        DestroyStickOnSlice();
    }

    void CollidingWithBuffer()
    {
        GameObject BufferGo = GameObject.FindGameObjectWithTag("Buffer");
        bool CollidIngWithBuffer = Physics2D.Linecast(BufferCollisionPoint.transform.position, BufferCollisionPoint2.transform.gameObject.transform.position, BufferLayerMask);
        bool CollidIngWithBuffer2 = Physics2D.Linecast(BufferCollisionPoint3.transform.position, BufferCollisionPoint4.transform.gameObject.transform.position, BufferLayerMask);

        if (CollidIngWithBuffer || CollidIngWithBuffer2)
        {
            Debug.Log("Sliced Stick");

            DestroyMe(BufferGo);
            _manager.ShowTextAnim = false;
            Collider2D StickCollider = _stick.gameObject.GetComponent<Collider2D>();
            if (StickCollider != null)
            {
                StickCollider.enabled = false;
            }

            //Debug.Log("Sliced stick at right position");

            _objStick = Instantiate(Tick, BufferGo.transform.parent.localPosition, Quaternion.identity);
            StartCoroutine(TakeAwayObject(_objStick));
            _sticksDestroyed++;

            _scoreManager.AddScore(1);
        }
    }
    void CollidingWithStick()
    {
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");
        bool CollidIngWithStick = Physics2D.Linecast(BufferCollisionPoint.transform.position, BufferCollisionPoint2.transform.gameObject.transform.position, LayerMask);
        bool CollidIngWithBuffer2 = Physics2D.Linecast(BufferCollisionPoint3.transform.position, BufferCollisionPoint4.transform.gameObject.transform.position, BufferLayerMask);

        if (CollidIngWithStick || CollidIngWithBuffer2)
        {
            Lives--;
            DestroyMe(stickObject);
            if (!_isCrossEnabled)
                _objCross = Instantiate(Cross, transform.position, Quaternion.identity);

            StartCoroutine(TakeAwayObject(_objCross));
            //return;
        }
    }

    void DestroyStickOnSlice()
    {
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");

        bool CollidIngWithBuffer = Physics2D.Linecast(BufferCollisionPoint.transform.position, BufferCollisionPoint2.transform.gameObject.transform.position, BufferLayerMask);
        bool CollidIngWithStick = Physics2D.Linecast(BufferCollisionPoint.transform.position, BufferCollisionPoint2.transform.gameObject.transform.position, LayerMask);

        bool CollidingWithStickObj = (CollidIngWithStick || CollidIngWithBuffer);
        if (CollidingWithStickObj)
        {
            StickSpawner.stickSpawner.HasWaveEnded = true;
            DestroyMe(stickObject);
        }
    }

    void DestroyMe(GameObject Gameobj)
    {
        Destroy(Gameobj, 4f);
    }

    IEnumerator TakeAwayObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        //Destroy(obj.gameObject);
    }
}
