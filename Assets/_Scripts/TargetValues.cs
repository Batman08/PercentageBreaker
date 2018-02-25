using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    public SpriteRenderer MyRenderer;
    private GameManager _gameManager;

    //private float _percentageNumValue;
    //private float _percentageMaxValue;
    private float StickmaxX;
    private float StickminX;
    //private float _changeBufferTime = 1;
    [HideInInspector]
    public float _bufferSizeResult;

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
        //Changes buffer size when the buffer percentage changes
        InvokeRepeating("ChangeBufferSize", _gameManager.ChangeBufferPercentageTime, _gameManager.ChangeBufferPercentageTime);
    }

    void FindComponents()
    {
        //Finds Game Manager
        _gameManager = FindObjectOfType<GameManager>();
        //Finds renderer bounds
        StickmaxX = MyRenderer.bounds.max.x;
        StickminX = MyRenderer.bounds.min.x;
    }


    void Update()
    {
        CalculateXPosition();
        //   ChangeBufferSize();
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
        //Get the percentage
        float Percentage = Mathf.Round(_gameManager.Percentage) / 100;
        //float transX = StickmaxX * Percentage;
        //Get the stick size and multiply that by the percentage
        float newTransformX = 1.1069f * Percentage;
        //Apply that to the transform local scale
        transform.localPosition = new Vector2(Mathf.Abs(newTransformX), transform.localPosition.y * 0);
    }

    void CalculateBufferSize()
    {
        //Get the buffer percentage
        float BufferPercentage = _gameManager.BufferPercent;

        //Get the size of the target(X Scale)
        float size = 0.1f;

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
