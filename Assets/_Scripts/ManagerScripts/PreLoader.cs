using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    private string SoundIsOnKey = "On";

    void Start()
    {
        PlayerPrefs.SetInt(SoundIsOnKey, 0);
        Debug.Log(PlayerPrefs.GetInt(SoundIsOnKey));
        SceneManager.LoadScene(1);
    }
}
