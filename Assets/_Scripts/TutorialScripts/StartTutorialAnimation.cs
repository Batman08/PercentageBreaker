using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartTutorialAnimation : MonoBehaviour
{
    public Animator Anim;
    public GameObject ReplayBtn;
    public GameObject ContinueBtn;

    private Stick stick;

    void Start()
    {
        stick = FindObjectOfType<Stick>();
        Anim = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Animator>();
        Anim.StopPlayback();
        ReplayBtn.SetActive(value: false);
    }

    public void ReplayAnim()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(3);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stick"))
        {
            stick.Move = false;
            stick.SpeedForce = 0;
            Anim.SetBool("HasShowedSlice", true);
            ReplayBtn.SetActive(value: true);
        }
    }
}