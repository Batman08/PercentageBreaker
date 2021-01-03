using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    [Space]

    public Transform Trail;
    [Space]

    public TrailRenderer TrailRend;
    [Space]

    [Header("Blade Cutting Variables")]
    //0.001 -- min cutting vel
    private float MinCuttingVelocity = 0.001f;

    private bool _isCutting = false;
    private Vector2 _previousePosition;
    private Rigidbody2D _rb2d;
    private Camera _cam;
    private GameObject _currentBladeTrail;
    private CircleCollider2D _circleCollider;
    private Vector2 _newPosition;
    private RaycastCollision _raycastCollision;

    void Awake()
    {
        Instances();

        TrailRend.startWidth = 0.18f;
        TrailRend.endWidth = 0.043f;
    }

    void Update()
    {
        _newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        InputControls();
    }

    void FixedUpdate()
    {
        ContrainBlade();
    }

    void ContrainBlade()
    {
        #region X Values
        float XMin = -2.95f;
        float XMax = 2.95f;
        #endregion

        #region Y Values
        float YMin = -4.417f;
        float YMax = 3.745f;
        #endregion

        #region X And Y Clamp Pos
        float x = Mathf.Clamp(_rb2d.position.x, XMin, XMax);
        float y = Mathf.Clamp(_rb2d.position.y, YMin, YMax);
        #endregion

        Vector2 ClampedPositions = new Vector2(x, y);
        _rb2d.position = ClampedPositions;
    }

    void Instances()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _circleCollider = GetComponent<CircleCollider2D>();
        _raycastCollision = GetComponent<RaycastCollision>();
        _circleCollider.enabled = false;
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

            //_raycastCollision.RaycastToSeeIfCollidingWithStick();
            //_raycastCollision.RayCastCollisions();
        }
    }

    void UpdateCut()
    {
        //Vector2 pos = _newPosition;
        //Trail.position = pos;
        EnableCollider();

        #region Gradient
        /*float alpha1 = 1.0f;
        float alpha2 = 0.0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys
            (
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 0.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha: alpha1, time: 0.0f), new GradientAlphaKey(alpha: alpha2, time: 0.0f) }
            );
        TrailRend.colorGradient = gradient;*/
        #endregion
    }

    void EnableCollider()
    {
        _rb2d.position = _newPosition;

        float velocity = (_newPosition - _previousePosition).magnitude * Time.deltaTime;
        if (velocity > MinCuttingVelocity)
        {
            _circleCollider.enabled = true;
        }
        else
        {
            _circleCollider.enabled = false;
        }

        _previousePosition = _newPosition;
    }

    private float NewVelocity(Vector2 newPosition)
    {
        return (newPosition - _previousePosition).magnitude / Time.deltaTime;
    }

    void StartCutting()
    {
        _isCutting = true;
        _rb2d.position = _newPosition;
        transform.position = _rb2d.position;
        _currentBladeTrail = Instantiate(BladeTrailPrefab, transform);
        _circleCollider.enabled = false;

        PlaySlashingSound();
    }

    void PlaySlashingSound()
    {
        float velocity = NewVelocity(_newPosition);
        string SlashSound = "SlashingSound";
        bool VelocityIsGreaterThanZero = (velocity > MinCuttingVelocity);
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (VelocityIsGreaterThanZero)
        {
            bool SoundIsNotEqualNull = (audioManager != null);
            if (SoundIsNotEqualNull)
            {
                audioManager.Play(SlashSound);
            }
        }
    }

    void StopCutting()
    {
        _isCutting = false;
        if (_currentBladeTrail != null)
        {
            _currentBladeTrail.transform.SetParent(null);
        }//2f
        Destroy(obj: _currentBladeTrail, t: 2f);
        _circleCollider.enabled = false;
    }
}
