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

        ChangeStickColour();

        //Debug.Log(g.Evaluate(0.25F));

        //spriterenderer.color = g.Evaluate(Random.Range(0f, 1f));

    }

    void ChangeStickColour()
    {
        Gradient g;
        GradientColorKey[] gck;
        GradientAlphaKey[] gak;
        g = new Gradient();
        gck = new GradientColorKey[2];
        gck[0].color = Color.red;
        gck[0].time = 0.0F;
        gck[1].color = Color.blue;
        gck[1].time = 1.0F;
        gak = new GradientAlphaKey[2];
        gak[0].alpha = 1.0F;
        gak[0].time = 0.0F;
        gak[1].alpha = 0.0F;
        gak[1].time = 1.0F;
        g.SetKeys(gck, gak);
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
