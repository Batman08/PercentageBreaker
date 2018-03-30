using UnityEngine;
using System.Collections.Generic;
using UnitySpriteCutter;
[RequireComponent(typeof(LineRenderer))]
public class LinecastCutter : MonoBehaviour
{
    public static LinecastCutter linecastCutter;

    public LayerMask layerMask;

    public Vector2 mouseStart;
    public Vector2 mouseEnd;

    private void Start()
    {
        linecastCutter = this;
    }

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        mouseEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetMouseButtonUp(0))
        //{
        //    int layermaskValue = layerMask.value;
        //    LinecastCut(mouseStart, mouseEnd, layermaskValue);
        //}

    }

    public void LinecastCut(Vector2 lineStart, Vector2 lineEnd, int layerMask = Physics2D.AllLayers)
    {
        Stick stick = FindObjectOfType<Stick>();
        List<GameObject> gameObjectsToCut = new List<GameObject>();
        RaycastHit2D[] hits = Physics2D.LinecastAll(lineStart, lineEnd, layerMask);
        foreach (RaycastHit2D hit in hits)
        {
            if (HitCounts(hit))
            {
                gameObjectsToCut.Add(hit.transform.gameObject);
            }
        }

        foreach (GameObject go in gameObjectsToCut)
        {
            SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
            {
                lineStart = lineStart,
                lineEnd = lineEnd,
                gameObject = go,
                gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
            });

            if (output != null && output.secondSideGameObject != null)
            {
                //Rigidbody2D newRigidbody = output.secondSideGameObject.AddComponent<Rigidbody2D>();
                //newRigidbody.velocity = output.firstSideGameObject.GetComponent<Rigidbody2D>().velocity;
                output.secondSideGameObject.transform.position = stick.transform.position;
                SlicedStick slicedStick = output.secondSideGameObject.AddComponent<SlicedStick>(); ;
            }
        }
    }

    bool HitCounts(RaycastHit2D hit)
    {
        return (hit.transform.GetComponent<SpriteRenderer>() != null ||
                 hit.transform.GetComponent<MeshRenderer>() != null);
    }

}
