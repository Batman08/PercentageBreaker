using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    public static TargetValues Values;

    public SpriteRenderer MyRenderer;
    private GameManager _gameManager;

    //private float _percentageNumValue;
    //private float _percentageMaxValue;
    private float StickmaxX;
    private float StickminX;
    //private float _changeBufferTime = 1;
    [HideInInspector]
    public float _bufferSizeResult;
    private Stick _stick;

    #region Completed_Methods (That_Work)
    void Awake()
    {
        FindComponents();
    }

    void OnEnable()
    {
        CheckIfRendererIsEnabled();
    }

    //void Start()
    //{
    //    //Changes buffer size when the buffer percentage changes
    //    InvokeRepeating("ChangeBufferSize", _gameManager.ChangeBufferPercentageTime, _gameManager.ChangeBufferPercentageTime);
    //}

    void FindComponents()
    {
        Values = this;
        //Finds Game Manager
        _gameManager = FindObjectOfType<GameManager>();
        _stick = FindObjectOfType<Stick>();
        //Finds renderer bounds
        StickmaxX = MyRenderer.bounds.max.x;
        StickminX = MyRenderer.bounds.min.x;

        bool ScaleIsAtTwentyPercent = (transform.localScale.x == 0.14f);
        if (ScaleIsAtTwentyPercent)
        {
            _gameManager.BufferPercent = _gameManager.MaxBufferValue;
            _gameManager.textbufferValue = 70;
            float x = 0.7f;
            float y = transform.localScale.y;
            transform.localScale = new Vector2(x, y);

            float speedIncrease = 0.10f;
            float NewStickSpeed = _stick.SpeedForce / speedIncrease;
            _stick.SpeedForce += NewStickSpeed;
        }
    }

    void LayerCollisions()
    {
        //Makes the stick GFX not collide with target
        //Because if sliced buffer it will collide with the stick as well
        int StickLayer = 8;
        int BufferLayer = 9;
        Physics2D.IgnoreLayerCollision(StickLayer, BufferLayer);
    }

    void FixedUpdate()
    {
        ClampPosition();
        CalculateXPosition();
        ChangeBufferSize();
    }

    void CheckIfRendererIsEnabled()
    {
        //Check if renderer is enabled
        //If not then find the componet
        //Then enable it
        if (MyRenderer == null)
        {
            MyRenderer = FindObjectOfType<Stick>().transform.GetChild(0).GetComponent<SpriteRenderer>();
            MyRenderer.enabled = true;
        }
    }

    void ClampPosition()
    {
        //Clamp x position
        float x = Mathf.Clamp(transform.position.x, StickminX, StickmaxX);
        float y = transform.position.y;

        if (transform.position.x > x || transform.position.x < x)
        {
            return;
        }

        //Then apply to the transform
        transform.position = new Vector2(x, y);

        //Check if target position is clamped between the min and max position
        //If not then clamp positions
        bool greaterThanMaxX = transform.position.x > StickmaxX;
        bool greaterThanMinX = transform.position.x < StickminX;
        if (greaterThanMaxX || greaterThanMinX)
        {
            ClampPosition();
        }
    }

    void CalculateXPosition()
    {
        float offset = 0.17096f;
        //Get the percentage
        float Percentage = Mathf.Round(_gameManager.Percentage) / 100;
        //float length = 0.75825f;
        float length = _gameManager.Length;

        //Get the stick size and multiply that by the percentage
        float newXTransform = length * Percentage + offset;

        //Apply that to the transform local scale
        transform.localPosition = new Vector2(Mathf.Abs(newXTransform), transform.localPosition.y * 0);
    }

    void CalculateBufferSize()
    {
        //Get the buffer percentage
        float BufferPercentage = _gameManager.BufferPercent;

        //Get the size of the target(X Scale)
        float size = 0.7f;

        //Then multiply the size by the buffer Percentage
        _bufferSizeResult = size * BufferPercentage;

        //Apply that to the transform local scale
        transform.localScale = new Vector2(_bufferSizeResult, transform.localScale.y);

    }

    void ChangeBufferSize()
    {
        CalculateBufferSize();
    }
    #endregion


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

    /*    void MinXScale()
        {
            //Keep scale at minimum scale
            float MinScale = 0.001f;
            bool XScaleIsLessThanMinScale = (transform.localScale.x <= MinScale);
            if (XScaleIsLessThanMinScale)
            {
                //Need to change Min scale because the collider doesn't work at that sie(0.001f)
                float minScalde = 0.003f;
                transform.localScale = new Vector2(minScalde, transform.localScale.y);
            }
        }*/
    #endregion
}
