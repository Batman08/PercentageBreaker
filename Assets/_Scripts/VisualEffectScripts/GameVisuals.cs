using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVisuals : MonoBehaviour
{
    private BladeSlicingMechanic _bladeSlicingMechanic;

    void Awake()
    {
        _bladeSlicingMechanic = FindObjectOfType<BladeSlicingMechanic>();

        Vector2 stickPos = _bladeSlicingMechanic.SlicedPos;
        transform.localPosition = stickPos;

        stickPos = Vector2.zero;
    }

    void Start()
    {
        StartCoroutine(DisableGameObject(2f));
    }

    IEnumerator DisableGameObject(float time)
    {
        yield return new WaitForSeconds(time);
        transform.position = Vector2.zero;
        Destroy(gameObject);
    }
}
