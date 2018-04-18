using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollision : MonoBehaviour
{
    public GameObject start1, end1;
    public GameObject start2, end2;
    public LayerMask LayerMask;

    public Vector2 _point1, _point2;
    public Vector2 _point3, _point4;

    void OnEnable()
    {
        SetRayCastingPosition();
    }

    void FixedUpdate()
    {
        Raycast();
    }

    void SetRayCastingPosition()
    {
        start1.gameObject.transform.position = _point1;
        end1.gameObject.transform.position = _point2;

        start2.gameObject.transform.position = _point3;
        end2.gameObject.transform.position = _point4;
    }

    void Raycast()
    {
        GameObject stickGo = GameObject.FindGameObjectWithTag("Stick");

        Debug.DrawLine(start1.gameObject.transform.position, end1.gameObject.transform.position, Color.cyan);
        Debug.DrawLine(start2.gameObject.transform.position, end2.gameObject.transform.position, Color.green);

        bool CollidingWithStick1 = Physics2D.Linecast(start1.gameObject.transform.position, end1.gameObject.transform.position, LayerMask);
        bool CollidingWithStick2 = Physics2D.Linecast(start2.gameObject.transform.position, end2.gameObject.transform.position, LayerMask);
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
}
