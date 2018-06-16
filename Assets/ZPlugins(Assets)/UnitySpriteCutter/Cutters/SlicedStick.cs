using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedStick : MonoBehaviour
{
    private Stick _stick;

    void Start()
    {
        _stick = FindObjectOfType<Stick>();
    }

    void Update()
    {
        if (_stick == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector2 leftPos = new Vector2(transform.position.x + Vector2.left.x / 2, _stick.transform.position.y);
        Vector2 rightPos = new Vector2(_stick.transform.position.x - Vector2.left.x / 2, _stick.transform.position.y);

        if (transform != null)
        {
            transform.position = new Vector2(leftPos.x, leftPos.y);
        }

        if (_stick.transform != null)
        {
            _stick.transform.position = new Vector2(rightPos.x, rightPos.y);
        }
    }
}
