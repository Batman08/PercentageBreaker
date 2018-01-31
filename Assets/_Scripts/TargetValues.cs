using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    //Need to fix x axis for buffer/target ---- 0.55345

    public SpriteRenderer MyRenderer;

    private GameManager _manager;

    //private float _XMax = 0.5535f;
    //private float _XMin = -0.5535f;
    private float _percentageNumValue;
    private float _percentageMaxValue = 100;
    float maxX;
    float minX;
    private float _changeBufferTime = 1;

    #region DoneMethods (That Work)
    void Awake()
    {


        _manager = FindObjectOfType<GameManager>();
        Debug.Log(MyRenderer.bounds.max.x);

        maxX = MyRenderer.bounds.max.x;
        minX = MyRenderer.bounds.min.x;

        //Debug.Log(_rend.x + " " + _rend.y);
    }

    void Start()
    {
        //StartCoroutine(ChangeBufferSize(_changeBufferTime));
        InvokeRepeating("ChangeBufferSize", 0, 1f);
    }

    void OnEnable()
    {
        if (MyRenderer == null)
        {
            MyRenderer = FindObjectOfType<Stick>().transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        //  StartCoroutine(ChangeBufferSize(_changeBufferTime));
    }

    void Update()
    {
        Debug.Log(transform.localScale.x);
        // RightXPosition();
        CalculateXPosition();

        _percentageNumValue = _manager.finalValue;

        //  CalculateObjectSize();
    }

    void FixedUpdate()
    {
        ClampPosition();

        bool greaterThanMaxX = transform.position.x > maxX;
        bool greaterThanMinX = transform.position.x < minX;

        if (greaterThanMaxX || greaterThanMinX)
        {
            ClampPosition();
        }

        //float billy_1 = Mathf.Clamp(200f, 5f, 10f);
        //float billy_2 = Mathf.Clamp(-200f, 5f, 10f);
    }

    void ClampPosition()
    {
        //float x = Mathf.Clamp(transform.position.x, 5f, 10f);
        //float x = Mathf.Clamp(transform.position.x, -_rend.x, _rend.x);
        //_rend.x = 1.1069f;
        //float minX = gameObject.transform.parent.position.x;

        float x = Mathf.Clamp(transform.position.x, minX, maxX);

        float y = transform.position.y;

        transform.position = new Vector2(x, y);

        //  Debug.Log(MyRenderer.bounds.max.x);
    }

    void CalculateObjectSize()
    {
        float calcPercentage = _percentageNumValue / _percentageMaxValue;
        ChangeBufferMargin(calcPercentage);
    }

    void ChangeBufferMargin(float currPercentage)
    {
        transform.localScale = new Vector3(currPercentage, transform.localScale.y, transform.localScale.z);
    }

    void CalculateXPosition()
    {
        float Percentage = Mathf.Round(_manager.finalValue) / 100;

        float transX = maxX * Percentage;
        float newTransformX = 1.1069f * Percentage;
        //Debug.Log(maxX);
        //Debug.Log(transX);
        transform.localPosition = new Vector2(Mathf.Abs(newTransformX), transform.localPosition.y * 0);
    }
    #endregion


    void ChangeBufferSize()
    {
        float percent = _manager.BufferPercent;
        float size = transform.localScale.x;
        float result = size * percent;


        ChangeBufferMargin(result);
    }


    #region FailedMethods
    void RightXPosition()
    {
        //float Pos = -0.55345f;
        //float MaxPos = 1.1069f;

        //float MaxTransformPos = 1.16845f;
        float MaxTransformPos = maxX;

        float Percentage = Mathf.Round(_manager.finalValue) / 100;
        float rightPos = MaxTransformPos * Percentage;

        Debug.Log(Percentage);
        Debug.Log(transform.position.x);

        transform.position = new Vector2(rightPos, transform.position.y);
    }
    #endregion
}
