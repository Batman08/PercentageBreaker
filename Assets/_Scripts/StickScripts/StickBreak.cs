using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBreak : MonoBehaviour
{
    public void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.AddComponent<Rigidbody>();
    }
}
