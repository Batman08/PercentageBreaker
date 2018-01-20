using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public SpriteRenderer spriterenderer;

    private GameManager manager;

    void Start()
    {
        spriterenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<GameManager>();



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
        Debug.Log(g.Evaluate(0.25F));

        //spriterenderer.color = g.Evaluate(Random.Range(0f, 1f));

    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, -0.01f, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool collidedWithBlade = (collision.CompareTag("Blade"));
        bool hitAtCorrectPosition = (Input.mousePosition.magnitude == manager.finalValue);
        bool collidedWithDeathZone = (collision.CompareTag("DeathZone"));

        if (collidedWithBlade)
        {
            // GameManager.Manager.ChangePercentage();
            Destroy(gameObject);
        }

        if (collidedWithBlade && hitAtCorrectPosition)
        {
            //ToDo: Instaniate a stick break effect
            Destroy(gameObject);
        }

        else if (collidedWithDeathZone)
        {
            Destroy(gameObject);
        }
    }

    #region Intersecting code(Not Used) 

    //Variables
    //---------
    //public GameObject m_MyObject, m_NewObject;
    //private Collider2D m_Collider, m_Collider2;

    //Start Method
    //------------
    /*
        //Check that the first GameObject exists in the Inspector and fetch the Collider
        if (m_MyObject != null)
            m_Collider = m_MyObject.GetComponent<Collider2D>();

        //Check that the second GameObject exists in the Inspector and fetch the Collider
        if (m_NewObject != null)
            m_Collider2 = m_NewObject.GetComponent<Collider2D>();*/

    //Update Method
    //--------------
    //Vector2 spriteWidth = spriterenderer.bounds.size;
    //Vector2 StickMin = spriterenderer.bounds.min;
    //Vector2 StickMax = spriterenderer.bounds.max;

    /*
    bool Intersecting = (m_Collider.bounds.Intersects(m_Collider2.bounds));
    bool Colliderenabled = (m_Collider);
    bool Collider2enabled = (m_Collider2);
    //If the first GameObject's Bounds enters the second GameObject's Bounds, output the message

    if (Intersecting)
    {
        m_NewObject.SetActive(value: true);
    }

    if (Intersecting)
    {
        Debug.Log("Bounds intersecting");
        Debug.Log(Intersecting);
        Destroy(gameObject);
    }*/
    #endregion
}
