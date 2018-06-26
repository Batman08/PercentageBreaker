using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSlicingMechanic : MonoBehaviour
{
    public GameObject Tick, Cross;

    private AudioManager _audioManager;
    private LivesManager _livesManager;
    private ScoreManager _scoreManager;

    public bool HasSlicedCorrectly;

    void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _livesManager = FindObjectOfType<LivesManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PerformSliceAnimation();
        DestroyStickGameObj(collision);
    }

    void PerformSliceAnimation()
    {
        int layermaskValue = LinecastCutter.linecastCutter.layerMask.value;
        LinecastCutter.linecastCutter.LinecastCut(LinecastCutter.linecastCutter.mouseStart, LinecastCutter.linecastCutter.mouseEnd, layermaskValue);
    }

    void DestroyStickGameObj(Collider2D collision)
    {
        bool slicedBuffer = (collision.GetComponent<Collider2D>().CompareTag("Buffer"));
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");

        if (slicedBuffer)
        {
            HasSlicedCorrectly = true;
            _livesManager._sticksDestroyed++;
            _scoreManager.AddScore();

            if (Tick != null)
            {
                Instantiate(Tick, collision.transform.position, Quaternion.identity);
            }

            DestroyStickSound(stickObject);
            Destroy(stickObject.GetComponent<PolygonCollider2D>());
        }

        else
        {
            if (Cross != null)
            {
                Instantiate(Cross, collision.transform.position, Quaternion.identity);
            }
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
}
