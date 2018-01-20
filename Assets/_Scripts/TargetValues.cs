using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValues : MonoBehaviour
{

    public SpriteRenderer MyRenderer;
    public GameObject StickObj;

    private GameManager _manager;

    private Vector2 rend;
    private float XMax = 0.5535f;
    private float XMin = -0.5535f;
    private float percentageNumValue;
    private float percentageMaxValue;

    void Awake()
    {
        _manager = FindObjectOfType<GameManager>();
        rend = MyRenderer.size;


        //RandomXPosition();
        Debug.Log(rend.x + " " + rend.y);
    }

    void Update()
    {
        percentageNumValue = _manager.finalValue;

        CalculateObjectSize();
    }

    void FixedUpdate()
    {
        ClampPosition();
    }

    void ClampPosition()
    {
        rend.x = 0.5535f;
        rend.y = 0;

        transform.position = new Vector2
       (
           Mathf.Clamp(transform.position.x, -rend.x, rend.x),
           Mathf.Clamp(transform.position.y, -rend.y, rend.y)
       );
    }

    void CalculateObjectSize()
    {
        float calcPercentage = percentageNumValue / percentageMaxValue;
        ChangeOBjectsSize(calcPercentage);
    }

    void ChangeOBjectsSize(float currPercentage)
    {
        transform.localScale = new Vector3(currPercentage, transform.localScale.y, transform.localScale.z);
    }

    void RandomXPosition()
    {
        float randPos = Random.Range(XMin, XMax);

        Debug.Log(randPos);

        transform.position = new Vector2(randPos, 0);
    }
}
