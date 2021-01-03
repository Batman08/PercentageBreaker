using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickColliderChecker : MonoBehaviour
{
    public Collider2D[] _stickCollider;

    public bool EnableCollider;

    private void Update()
    {
        if (EnableCollider)
        {
            SetAllCollidersStatus(true);
        }

        else if (!EnableCollider)
        {
            SetAllCollidersStatus(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableCollider(collision);
    }

    private void DisableCollider(Collider2D collision)
    {
        bool hasCollidedWithStick = (collision.tag == "BladeChecker");
        bool hasCollider = (_stickCollider != null);

        if (hasCollider)
        {
            if (hasCollidedWithStick)
            {
                //_stickCollider.enabled = false;

                EnableCollider = false;
            }

            else
            {
                EnableCollider = true;
            }
        }
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider2D c in _stickCollider)
        {
            c.enabled = active;
        }
    }
}
