using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject BladeTrailPrefab;
    public float MinCuttingVelocity = 0.000000000f;
    public float CuttingRadius = 1;

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
            //Cut();
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
        newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        _rb2d.position = newPosition;

        float velocity = NewVelocity(newPosition);

        if (velocity > MinCuttingVelocity)
        {
            _circleCollider.enabled = true;
            //CuttingPhysics();
        }
        else
        {
            _circleCollider.enabled = true;
        }

        _previousePosition = newPosition;
    }

    private float NewVelocity(Vector2 newPosition)
    {
        return (newPosition - _previousePosition).magnitude * Time.deltaTime;
    }

    void StartCutting()
    {
        newPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
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
    #region Raycast (Not Being Used)
    void Cut()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Stick")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
    #endregion
}
