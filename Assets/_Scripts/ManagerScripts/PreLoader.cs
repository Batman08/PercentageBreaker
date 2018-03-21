using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(0);
    }
}
