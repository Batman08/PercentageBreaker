using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    public float MinCuttingVelocity = 0.000001f;

    private bool _isCutting = false;
    private Vector2 _previousePosition;
    private Rigidbody2D _rb2d;
    private Camera _cam;
    private GameObject _currentBladeTrail;
    private CircleCollider2D _circleCollider;

    void Awake()
    {
        Instances();
    }

    void Update()
    {
        InputControls();
    }

    void Instances()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    void InputControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }

        else if (Input.GetMouseButtonUp(0))
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
        Vector2 newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        _rb2d.position = newPosition;

        float velocity = NewVelocity(newPosition);

        if (velocity > MinCuttingVelocity)
        {
            _circleCollider.enabled = true;
        }
        else
        {
            _circleCollider.enabled = false;
        }

        _previousePosition = newPosition;
    }

    private float NewVelocity(Vector2 newPosition)
    {
        return (newPosition - _previousePosition).magnitude * Time.deltaTime;
    }

    void StartCutting()
    {
        Vector2 newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        _isCutting = true;
        _currentBladeTrail = Instantiate(BladeTrailPrefab, transform);
        _previousePosition = newPosition;
        _circleCollider.enabled = false;
    }

    void StopCutting()
    {
        _isCutting = false;
        _currentBladeTrail.transform.SetParent(null);
        Destroy(_currentBladeTrail, 2f);
        _circleCollider.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool destroyStick = (collision.collider.CompareTag("Stick"));

        if (destroyStick)
        {
            Destroy(collision.gameObject);
        }
    }
}
