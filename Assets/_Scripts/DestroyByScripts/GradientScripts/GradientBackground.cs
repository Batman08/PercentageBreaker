using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientBackground : MonoBehaviour
{
    public Material GradientMaterial;

    void Start()
    {
        //Gradient();
    }
    void Gradient()
    {
        GradientMaterial.SetColor("_Color", Color.blue); // the left color
        GradientMaterial.SetColor("_Color2", Color.red); // the right color
    }
}
