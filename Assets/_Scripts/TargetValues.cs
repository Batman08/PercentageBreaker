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
    [HideInInspector]
    public float _result;

    #region Completed_Methods (That_Work)
    void Awake()
    {
        FindComponents();
    }

    void OnEnable()
    {
        CheckIfRendererIsEnabled();
    }

    void Start()
    {
        //   StartCoroutine(ChangeBufferSize());
        InvokeRepeating("ChangeBufferSize", 0, _manager.ChangeBufferPercentTime);
    }

    void FindComponents()
    {
        _manager = FindObjectOfType<GameManager>();
        maxX = MyRenderer.bounds.max.x;
        minX = MyRenderer.bounds.min.x;
    }


    void Update()
    {
        CalculateXPosition();
        MinXScale();
        //   ChangeBufferSize();
    }

    void LayerCollisions()
    {
        int StickLayer = 8;
        int TargetLayer = 9;
        Physics2D.IgnoreLayerCollision(StickLayer, TargetLayer);
    }

    void MinXScale()
    {
        if (transform.localScale.x <= 0.001f)
        {
            float minScale = 0.003f;
            transform.localScale = new Vector2(minScale, transform.localScale.y);
        }
    }

    void FixedUpdate()
    {
        ClampPosition();
    }

    void CheckIfRendererIsEnabled()
    {
        if (MyRenderer == null)
            MyRenderer = FindObjectOfType<Stick>().transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void ClampPosition()
    {
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float y = transform.position.y;

        transform.position = new Vector2(x, y);

        bool greaterThanMaxX = transform.position.x > maxX;
        bool greaterThanMinX = transform.position.x < minX;
        if (greaterThanMaxX || greaterThanMinX)
        {
            ClampPosition();
        }
    }

    void CalculateXPosition()
    {
        float Percentage = Mathf.Round(_manager.finalValue) / 100;
        float transX = maxX * Percentage;
        float newTransformX = 1.1069f * Percentage;

        transform.localPosition = new Vector2(Mathf.Abs(newTransformX), transform.localPosition.y * 0);
    }
    #endregion


    void CalculateBufferSize()
    {
        float percent = _manager.BufferPercent;
        float size = 0.1f;
        _result = size * percent;
        //float finalResult = size - result;

        Debug.Log(percent);
        Debug.Log(_result);

        transform.localScale = new Vector2(_result, transform.localScale.y);
    }

    void ChangeBufferSize()
    {
        CalculateBufferSize();
    }

    #region Methods_That_Are_Not_Needed_Yet
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
