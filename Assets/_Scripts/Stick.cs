using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public SpriteRenderer spriterenderer;
    public bool Move;
    //-0.01f
    public float SpeedForce;

    private GameManager manager;

    void Start()
    {
        spriterenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        CheckForChild();
    }

    void FixedUpdate()
    {
        MoveStick();
    }

    void CheckForChild()
    {
        bool NoChildren = (transform.childCount <= 1);
        if (NoChildren)
        {
            Destroy(gameObject);
        }
    }

    void MoveStick()
    {
        if (Move)
            transform.Translate(new Vector3(0, -SpeedForce, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            Destroy(gameObject);
        }
    }
}
