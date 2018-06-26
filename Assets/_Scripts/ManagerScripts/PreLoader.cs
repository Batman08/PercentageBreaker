using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreLoader : MonoBehaviour
{
    private string SoundIsOnKey = "On";
    private string ShowTutorialKey = "On2";


    void Start()
    {
        PlayerPrefs.SetInt(SoundIsOnKey, 0);
        PlayerPrefs.SetInt(ShowTutorialKey, 0);
        Debug.Log(PlayerPrefs.GetInt(SoundIsOnKey));
        Debug.Log(PlayerPrefs.GetInt(ShowTutorialKey));
        SceneManager.LoadScene(1);
    }
}
