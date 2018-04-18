using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    private string SoundIsOnKey = "On";

    void Start()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetFloat(SoundIsOnKey, 0);
    }
}
