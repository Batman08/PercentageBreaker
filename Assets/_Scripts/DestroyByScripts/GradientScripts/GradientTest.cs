using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientTest : MonoBehaviour {

	public Gradient grad;
	public float value = 0f;

	public Color vColor = Color.white;

	GradientColorKey[] gCKeys;
	GradientAlphaKey[] gAKeys;

	void Update()
	{
		grad = new Gradient ();
		UpdateGradient();
		// Debug.Log(grad.Evaluate(0f)); // Uncomment to verify that color does change.
	}

	public void UpdateGradient () {

		gCKeys = grad.colorKeys; // store color keys
		gAKeys = grad.alphaKeys; // store alpha keys

		float nValue = value / 100f + 0.5f; // normalize that variable going from -50 to 50
		gCKeys[0].color.r = nValue; // make color vary with any formula here.
		gCKeys[0].color.g = nValue;
		gCKeys[0].color.b = nValue;
		vColor = gCKeys[0].color; // just to see the color in the Inspector
		grad.SetKeys(gCKeys, gAKeys); // set color and alpha keys
	}
}
