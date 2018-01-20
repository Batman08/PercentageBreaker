using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{
    //Need to fix x axis for buffer/target

    public SpriteRenderer MyRenderer;

    private GameManager _manager;

    private Vector2 _rend;
    private float _XMax = 0.5535f;
    private float _XMin = -0.5535f;
    private float _percentageNumValue;
    private float _percentageMaxValue = 100;
    private float _changeBufferTime = 5;

    void Awake()
    {
        _manager = FindObjectOfType<GameManager>();

        _rend = MyRenderer.size;

        Debug.Log(_rend.x + " " + _rend.y);
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
        RightXPosition();

        _percentageNumValue = _manager.finalValue;

        //  CalculateObjectSize();
    }

    void FixedUpdate()
    {
        ClampPosition();
    }

    void ClampPosition()
    {
        _rend.x = 1.1069f;

        transform.position = new Vector2
       (
           Mathf.Clamp(transform.position.x, 0, _rend.x),
          transform.position.y
       );
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

    void RightXPosition()
    {
        //float Pos = -0.55345f;
        float Pos = 1.1069f;
        float Percentage = Mathf.Round(_manager.finalValue) / 100;
        float rightPos = Pos * Percentage;
        Debug.Log(Percentage);

        transform.position = new Vector2(rightPos, transform.position.y);
    }

    IEnumerator ChangeBufferSize(float time)
    {
        float percent = 0.9f;
        float size = transform.localScale.x;
        float result = size * percent;


        yield return new WaitForSeconds(time);
        ChangeBufferMargin(result);
    }
}
