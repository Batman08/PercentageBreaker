using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public GameObject Cross;
    public Material GradientMaterial;
    public bool Move;
    //-0.01f

    public float SpeedForce;
    //0.024
    public string StickName;

    private GameObject _obj;

    private BladeSlicingMechanic _bladeSlicingMechanic;
    private PlayerVisualCorrection _playerVisualCorrection;

    public string CrossName = "RedCrossV2";

    private Collider2D _stickCollider;

    public bool DisableArrow;
    public GameObject Arrow;

    private SlicedStick[] _slicedStick;

    void Awake()
    {
        _bladeSlicingMechanic = FindObjectOfType<BladeSlicingMechanic>();
        _playerVisualCorrection = FindObjectOfType<PlayerVisualCorrection>();

        if (StickName != "MenuStick")
        {
            Cross = _playerVisualCorrection.transform.GetChild(0).transform.GetChild(1).gameObject;
        }

        _stickCollider = GetComponent<Collider2D>();

        SpeedForce = PlayerPrefs.GetFloat("Speed");
        // Gradient();
        //if (StickSpawner.stickSpawner != null)
        //{
        //    SpeedForce = StickSpawner.stickSpawner.Speed;
        //}
    }

    void OnEnable()
    {
        int BladeLayer = 11;
        int BufferLayer = 9;
        int StickLayer = 8;
        Physics2D.IgnoreLayerCollision(BladeLayer, BufferLayer, false);
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
    }

    public Color StickColor;

    public GameObject Buffer;

    void Update()
    {
        if (DisableArrow)
        {
            _slicedStick = FindObjectsOfType<SlicedStick>();
            //_slicedStick.gameObject.GetComponent<MeshRenderer>().material = Material;
            //GetComponent<MeshRenderer>().material = Material;
            foreach (SlicedStick stick in _slicedStick)
            {
                stick.GetComponent<MeshRenderer>().material.color = StickColor;
            }
            if (GetComponent<MeshRenderer>() != null)
            {
                GetComponent<MeshRenderer>().material.color = StickColor;
            }

            foreach (Collider2D collider in Buffer.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
        }

        CheckForChild();

        if (SpeedForce >= 0.5f)
        {
            SpeedForce = 0.5f;
        }

        PolygonCollider2D collider2D = GetComponent<PolygonCollider2D>();
        if (collider2D != null)
        {
            Destroy(collider2D);
        }

        if (DisableArrow)
        {
            Arrow.SetActive(value: false);
        }
    }

    void FixedUpdate()
    {
        MoveStick();
        LayerCollisions();
    }

    void Gradient()
    {
        GradientMaterial.SetColor("_Color", Color.white); // the left color
        GradientMaterial.SetColor("_Color2", Color.black); // the right color
    }

    void LayerCollisions()
    {
        //Makes the stick GFX not collide with target
        //Because if sliced buffer it will collide with the stick as well
        int StickLayer = 8;
        int BufferLayer = 9;
        Physics2D.IgnoreLayerCollision(StickLayer, BufferLayer);
    }

    void CheckForChild()
    {
        bool NoChildren = (transform.childCount <= 1);
        if (NoChildren)
        {
            Destroy(gameObject);
        }
    }

    void MoveStick()
    {
        if (Move)
        {
            //   transform.Translate(new Vector3(0, -SpeedForce, 0));
            //transform.position = Vector2.down * -SpeedForce * Time.deltaTime;
            transform.Translate(Vector2.down * SpeedForce, Space.World);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            SetPlayerVisualPositions(collision);
        }
    }

    void SetPlayerVisualPositions(Collider2D collider)
    {
        float yPosOffset = -4.072f;
        Vector2 collisionPos = collider.transform.position;
        Vector2 SlicedPosNew = new Vector2(collisionPos.x, yPosOffset);

        _bladeSlicingMechanic.SlicedPos = SlicedPosNew;

        if (StickSpawner.stickSpawner != null)
        {
            StickSpawner.stickSpawner.HasWaveEnded = true;
        }

        Cross.SetActive(value: true);

        Destroy(gameObject);
    }

    IEnumerator TakeAwayObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj.gameObject);
    }
}
