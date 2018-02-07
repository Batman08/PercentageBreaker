using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    public SpriteRenderer MyRenderer;
    private GameManager _manager;

    private float _percentageNumValue;
    private float _percentageMaxValue;
    private float maxX;
    private float minX;
    private float _changeBufferTime = 1;

    #region DoneMethods (That Work)
    void Awake()
    {
        _manager = FindObjectOfType<GameManager>();
        maxX = MyRenderer.bounds.max.x;
        minX = MyRenderer.bounds.min.x;
    }

    void Start()
    {
        StartCoroutine(ChangeBufferSize());
    }

    void OnEnable()
    {
        if (MyRenderer == null)
            MyRenderer = FindObjectOfType<Stick>().transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Debug.Log(transform.localScale.x);
        CalculateXPosition();
        ChangeBufferSize();
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
    }

    void ClampPosition()
    {
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float y = transform.position.y;

        transform.position = new Vector2(x, y);
    }

    void CalculateXPosition()
    {
        float Percentage = Mathf.Round(_manager.finalValue) / 100;
        float transX = maxX * Percentage;
        float newTransformX = 1.1069f * Percentage;

        transform.localPosition = new Vector2(Mathf.Abs(newTransformX), transform.localPosition.y * 0);
    }
    #endregion


    IEnumerator ChangeBufferSize()
    {
        yield return new WaitForSeconds(2);
        float percent = _manager.BufferPercent;
        float size = transform.localScale.x;
        float result = size * percent;
        float finalResult = size - result;

        Debug.Log(percent);
        Debug.Log(result);

        transform.localScale = new Vector2(finalResult, transform.localScale.y);
    }


    #region FailedMethods
    //void RightXPosition()
    //{
    //    //float Pos = -0.55345f;
    //    //float MaxPos = 1.1069f;

    //    //float MaxTransformPos = 1.16845f;
    //    float MaxTransformPos = maxX;

    //    float Percentage = Mathf.Round(_manager.finalValue) / 100;
    //    float rightPos = MaxTransformPos * Percentage;

    //    Debug.Log(Percentage);
    //    Debug.Log(transform.position.x);

    //    transform.position = new Vector2(rightPos, transform.position.y);
    //}
    //void CalculateObjectSize()
    //{
    //    float calcPercentage = _percentageNumValue / _percentageMaxValue;
    //    ChangeBufferMargin(calcPercentage);
    //}

    //void ChangeBufferMargin(float currPercentage)
    //{
    //    transform.localScale = new Vector2(currPercentage, transform.localScale.y);
    //}
    #endregion
}
