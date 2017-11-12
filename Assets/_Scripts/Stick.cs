using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public GameObject m_MyObject, m_NewObject;
    Collider2D m_Collider, m_Collider2;

    void Start()
    {
        //Check that the first GameObject exists in the Inspector and fetch the Collider
        if (m_MyObject != null)
            m_Collider = m_MyObject.GetComponent<Collider2D>();

        //Check that the second GameObject exists in the Inspector and fetch the Collider
        if (m_NewObject != null)
            m_Collider2 = m_NewObject.GetComponent<Collider2D>();
    }

    void Update()
    {
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
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool collidedWithBlade = (collision.CompareTag("Blade"));
        bool collidedWithDeathZone = (collision.CompareTag("DeathZone"));


        if (collidedWithBlade)
        {
            //ToDo: Instaniate a stick break effect
        }

        else if (collidedWithDeathZone)
        {
            Destroy(gameObject);
        }
    }
}
