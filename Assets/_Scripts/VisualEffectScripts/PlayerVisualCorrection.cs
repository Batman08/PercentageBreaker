using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualCorrection : MonoBehaviour
{
    public GameObject EffectHolder;

    public bool CanInstantiate = false;
    void Awake()
    {
        Instantiate(original: EffectHolder, parent: transform);
    }

    public void CheckForChildren()
    {
        bool checkIfVisualsHaveBeenInstantiated = (transform.GetChild(0).childCount < 2) && CanInstantiate;
        bool checkWhatHasntBeenInstantiated = (transform.GetChild(0).tag == "Tick") && CanInstantiate;

        if (checkIfVisualsHaveBeenInstantiated)
        {
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(original: EffectHolder, parent: transform);

            CanInstantiate = false;
        }
    }

    #region Longer Way Of Checking
    /*
    void CheckChildren(GameObject tick, GameObject cross)
    {
        bool checkIfVisualsHaveBeenInstantiated = (transform.childCount < 2);
        bool checkWhatHasntBeenInstantiated = (transform.GetChild(0).tag == "Tick") && CanInstantiate;

        if (checkIfVisualsHaveBeenInstantiated)
        {
            CanInstantiate = true;

            if (checkWhatHasntBeenInstantiated)
            {
                if (CanInstantiate)
                {
                    InstantiateObject(Cross);
                }
            }

            else
            {
                if (CanInstantiate)
                {
                    InstantiateObject(Tick);
                }
            }
        }

        if (transform.childCount > 2)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void InstantiateObject(GameObject VisualObject)
    {
        if (CanInstantiate)
        {
            Instantiate(original: VisualObject, parent: transform);
            CanInstantiate = false;
        }
    }
    */
    #endregion
}
