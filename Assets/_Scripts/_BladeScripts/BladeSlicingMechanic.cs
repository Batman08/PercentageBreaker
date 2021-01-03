using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSlicingMechanic : MonoBehaviour
{
    public GameObject VisualEffects;

    private AudioManager _audioManager;
    private LivesManager _livesManager;
    private ScoreManager _scoreManager;

    public bool HasSlicedCorrectly;

    public int ScoreValue;

    private PlayerVisualCorrection _playerVisualCorrection;

    void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        _livesManager = FindObjectOfType<LivesManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _playerVisualCorrection = FindObjectOfType<PlayerVisualCorrection>();

        Instantiate(VisualEffects, Vector2.zero, Quaternion.identity);
        VisualEffects = GameObject.Find("VisualEffects(Clone)");

        ScoreValue = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PerformSliceAnimation();
        SlicedPos = collision.transform.position;
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
        bool slicedStick = (collision.GetComponent<Collider2D>().CompareTag("Stick"));
        GameObject stickObject = GameObject.FindGameObjectWithTag("Stick");

        if (slicedBuffer)
        {
            HasSlicedCorrectly = true;
            _livesManager._sticksDestroyed++;
            _scoreManager.AddScore(ScoreValue);

            VisualEffects.transform.GetChild(0).GetChild(0).gameObject.SetActive(value: true);
        }

        else if (slicedStick)
        {
            _livesManager.Lives--;
            VisualEffects.transform.GetChild(0).GetChild(1).gameObject.SetActive(value: true);
        }

        DestroyStickSound(stickObject);
        Destroy(stickObject.GetComponent<PolygonCollider2D>());

        SlicedPos = collision.transform.position;
    }
    public Vector2 SlicedPos;
    void DestroyStickSound(GameObject Gameobj)
    {

        bool StickSlicedSoundIsEnabled = _audioManager != null;
        if (StickSlicedSoundIsEnabled)
        {
            string SlashSound = "StickSlicedSound";
            _audioManager.Play(SlashSound);
        }

        Destroy(Gameobj, 2f);
    }
}
