using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliders : MonoBehaviour
{
    public LayerMask LayerMask;

    public GameObject StickGameObject;
    public GameObject BladeGameObject;

    private readonly string StickTag = "Stick";
    private readonly string BladeTag = "Blade";

    private int BladeLayer = 11;
    private int BladeCheckerLayer = 12;

    private Collider2D _collider2D;
    public bool hasCollided;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        //_stickCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        StickGameObject = GameObject.FindGameObjectWithTag(StickTag);
        if (StickGameObject != null)
        {
            StickCollider = StickGameObject.GetComponent<Collider2D>();
        }
    }


    void Raycast()
    {
        if (BladeGameObject != null)
        {
            Vector2 bladePos = BladeGameObject.transform.position;


            Vector2 rightPos = Vector2.right / 2f;
            Vector2 leftPos = Vector2.left / 2f;
            Vector2 upPos = Vector2.up / 2f;
            Vector2 downPos = Vector2.down / 2f;

            bool hasCollidedWithStickRight = Physics2D.Linecast(bladePos, bladePos + rightPos, LayerMask);
            bool hasCollidedWithStickLeft = Physics2D.Linecast(bladePos, bladePos + leftPos, LayerMask);
            bool hasCollidedWithStickUp = Physics2D.Linecast(bladePos, bladePos + upPos, LayerMask);
            bool hasCollidedWithStickDown = Physics2D.Linecast(bladePos, bladePos + downPos, LayerMask);



            Debug.DrawLine(bladePos, bladePos + rightPos, Color.red);
            Debug.DrawLine(bladePos, bladePos + leftPos, Color.red);
            Debug.DrawLine(bladePos, bladePos + upPos, Color.red);
            Debug.DrawLine(bladePos, bladePos + downPos, Color.red);

            bool stickIsEnabled = StickGameObject != null;
            bool stickColliderIsEnabled = (stickIsEnabled && StickCollider != null);
            bool hasCollided = (hasCollidedWithStickRight || hasCollidedWithStickLeft || hasCollidedWithStickUp || hasCollidedWithStickDown);
            if (stickColliderIsEnabled)
            {
                if (hasCollided)
                {
                    StickCollider.enabled = false;
                }

                else
                {
                    StickCollider.enabled = true;
                }
            }
        }
    }


    private void StickStuff()
    {

        bool isStickGameObjectActive = (StickGameObject != null && StickCollider != null);
        if (isStickGameObjectActive)
        {
            StickCollider = StickGameObject.GetComponent<Collider2D>();
        }

        Physics2D.IgnoreLayerCollision(BladeLayer, BladeCheckerLayer);

        bool hasCollider = (StickCollider != null);
        if (hasCollider)
        {
            if (hasCollided)
            {
                StickCollider.enabled = false;
            }
            else
            {
                StickCollider.enabled = true;
            }
        }
    }

    private void EnableBladeCheckerCollider()
    {
        bool hasPressedMouseButtonDown = (Input.GetMouseButtonDown(0));
        bool isMouseButtonUp = (Input.GetMouseButtonUp(0));
        if (hasPressedMouseButtonDown)
        {
            _collider2D.enabled = true;
        }

        else if (isMouseButtonUp)
        {
            _collider2D.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        BladeGameObject = GameObject.FindGameObjectWithTag(BladeTag);
        if (BladeGameObject != null)
        {
            transform.position = BladeGameObject.transform.position;
        }

        Raycast();
    }

    public Collider2D StickCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Buffer")
        //{
        //    DisableCollider(collision);
        //}
    }

    private void DisableCollider(Collider2D collision)
    {
        bool hasCollidedWithStick = (collision.tag == "Buffer");

        if (hasCollidedWithStick)
        {
            hasCollided = true;
        }
    }

}
