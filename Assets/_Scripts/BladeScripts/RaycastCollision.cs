using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollision : MonoBehaviour
{
    public GameObject BufferCollisionPoint, BufferCollisionPoint2;
    public GameObject start1, end1;
    public GameObject start2, end2;
    public LayerMask LayerMask;
    public LayerMask BufferLayerMask;

    public Vector2 BufferCollisionPointPos, BufferCollisionPointPos2;
    public Vector2 _point1, _point2;
    public Vector2 _point3, _point4;

    void OnEnable()
    {
        SetRayCastingPosition();
    }

    void FixedUpdate()
    {
        RaycastToSeeIfCollidingWithStick();
        CheckForBufferCollisions();
    }

    void SetRayCastingPosition()
    {
        //BufferCollisionPoint.gameObject.transform.position = BufferCollisionPointPos;
        //BufferCollisionPoint2.gameObject.transform.position = BufferCollisionPointPos2;

        start1.transform.position = _point1;
        end1.transform.position = _point2;

        start2.transform.position = _point3;
        end2.transform.position = _point4;
    }

    void RaycastToSeeIfCollidingWithStick()
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

    void CheckForBufferCollisions()
    {
        GameObject BufferGo = GameObject.FindGameObjectWithTag("Buffer");

        Debug.DrawLine(BufferCollisionPoint.transform.position, BufferCollisionPoint2.gameObject.transform.position, Color.blue);

        bool CollidIngWithBuffer = Physics2D.Linecast(BufferCollisionPoint.transform.position, BufferCollisionPoint2.transform.gameObject.transform.position, BufferLayerMask);

        if (CollidIngWithBuffer)
        {
            Debug.Log("Sliced Stick");
        }
    }
}
