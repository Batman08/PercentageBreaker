using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Material GradientMaterial;
    public SpriteRenderer spriterenderer;
    public bool Move;
    //-0.01f
    public float SpeedForce;

    private GameManager manager;

    void Start()
    {
        Gradient();
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
        LayerCollisions();
    }

    void Gradient()
    {
        GradientMaterial.SetColor("_Color", Color.white); // the left color
        GradientMaterial.SetColor("_Color2", Color.black); // the right color
    }

    void LayerCollisions()
    {
        //Makes the stick GFX not collide with target
        //Because if sliced buffer it will collide with the stick as well
        int StickLayer = 8;
        int BufferLayer = 9;
        Physics2D.IgnoreLayerCollision(StickLayer, BufferLayer);
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
