using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollision : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject start1;
    [Space]
    public GameObject start2;
    [Space]
    public GameObject end1;
    [Space]
    public GameObject end2;

    [Space]

    [Header("LayerMasks")]
    public LayerMask LayerMask;

    [Space]

    [Header("Vectors")]
    public Vector2 _point1;
    [Space]
    public Vector2 _point2;
    [Space]
    public Vector2 _point3;
    [Space]
    public Vector2 _point4;

    void OnEnable()
    {
        SetRayCastingPosition();
    }

    void FixedUpdate()
    {
        RaycastToSeeIfCollidingWithStick();
    }

    void SetRayCastingPosition()
    {
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

        bool collidingWithStick1 = Physics2D.Linecast(start1.transform.position, end1.transform.position, LayerMask);
        bool collidingWithStick2 = Physics2D.Linecast(start2.transform.position, end2.transform.position, LayerMask);
        bool stickIsEnabled = stickGo != null;
        bool stickColliderIsEnabled = (stickIsEnabled && stickGo.GetComponent<BoxCollider2D>() != null);

        if (collidingWithStick1 && stickIsEnabled || collidingWithStick2 && stickIsEnabled)
        {
            if (stickColliderIsEnabled)
            {
                stickGo.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        else if (!collidingWithStick1 && !collidingWithStick2 && stickIsEnabled)
        {
            if (stickColliderIsEnabled)
            {
                stickGo.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
