using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene(0);
    }
}
