using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSlicingMechanic : MonoBehaviour
{
    public GameObject _tick, _cross;

    private AudioManager _audioManager;
    private LivesManager _livesManager;

    void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _livesManager = FindObjectOfType<LivesManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyStickGameObj(collision);
    }

    void DestroyStickGameObj(Collider2D collision)
    {
        bool SlicedBuffer = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");

        if (SlicedBuffer)
        {
            stickObject.GetComponent<Collider2D>().enabled = false;
            _livesManager._sticksDestroyed++;


            //FINISH VISUAL EFFECTS FOR PLAYER TO RELISE WHAT THEY HAVE DONE!!
            //(TICK/CROSS)


            DestroyStickSound(stickObject);
        }
    }

    void DestroyStickSound(GameObject Gameobj)
    {

        bool StickSlicedSoundIsEnabled = _audioManager != null;
        if (StickSlicedSoundIsEnabled)
        {
            string SlashSound = "StickSlicedSound";
            _audioManager.Play(SlashSound);
        }

        Destroy(Gameobj, 4f);
    }

    void DidSlicecorrectlyVisual(Collider2D collision, GameObject tick, GameObject cross)
    {
        bool SlicedBuffer = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));

        if (SlicedBuffer)
        {
            tick.SetActive(value: true);
            cross.SetActive(value: false);
        }
    }
}
