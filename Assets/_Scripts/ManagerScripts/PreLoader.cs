using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    private string SoundIsOnKey = "On";

    void Awake()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetFloat(SoundIsOnKey, 0);
    }
}
