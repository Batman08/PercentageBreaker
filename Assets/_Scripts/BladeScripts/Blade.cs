using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    [Space]
    [Header("Blade Cutting Variables")]
    //0.001 -- min cutting vel
    public float MinCuttingVelocity = 0.00001f;

    private bool _isCutting = false;
    private Vector2 _previousePosition;
    private Rigidbody2D _rb2d;
    private Camera _cam;
    private GameObject _currentBladeTrail;
    private CircleCollider2D _circleCollider;
    private Vector2 newPosition;

    void Awake()
    {
        Instances();
    }

    void Update()
    {
        InputControls();
        newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Instances()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    void InputControls()
    {
        bool MouseButtonIsDown = (Input.GetMouseButtonDown(0));
        bool MouseButtonIsUp = (Input.GetMouseButtonUp(0));
        bool GameIsNotPaused = PauseMenuManager.GameIsPaused == false;

        if (MouseButtonIsDown & GameIsNotPaused)
        {
            StartCutting();
        }

        else if (MouseButtonIsUp & GameIsNotPaused)
        {
            StopCutting();
        }

        if (_isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        EnableCollider();
    }

    void EnableCollider()
    {

        _rb2d.position = newPosition;
        _circleCollider.enabled = true;


        _previousePosition = newPosition;
    }

    private float NewVelocity(Vector2 newPosition)
    {
        return (newPosition - _previousePosition).magnitude * Time.deltaTime;
    }

    void StartCutting()
    {
        //newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        _isCutting = true;
        _currentBladeTrail = Instantiate(BladeTrailPrefab, transform);
        // _previousePosition = newPosition;
        _circleCollider.enabled = false;

        PlaySlashingSound();
    }

    void PlaySlashingSound()
    {
        float velocity = NewVelocity(newPosition);
        bool VelocityIsGreaterThanZero = (velocity > MinCuttingVelocity);
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (VelocityIsGreaterThanZero)
        {
            bool SoundIsNotEqualNull = (audioManager != null);
            if (SoundIsNotEqualNull)
            {
                audioManager.Play("SlashingSound");
            }
        }
    }

    void StopCutting()
    {
        _isCutting = false;
        if (_currentBladeTrail != null)
        {
            _currentBladeTrail.transform.SetParent(null);
        }
        Destroy(_currentBladeTrail, 2f);
        _circleCollider.enabled = false;
    }
}
