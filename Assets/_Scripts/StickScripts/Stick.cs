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

    private BladeCollisions _blade;
    private GameObject _obj;

    void OnEnable()
    {
        int BladeLayer = 11;
        int BufferLayer = 9;
        int StickLayer = 8;
        Physics2D.IgnoreLayerCollision(BladeLayer, BufferLayer, false);
        Physics2D.IgnoreLayerCollision(BladeLayer, StickLayer, false);
    }

    void Start()
    {
        // Gradient();
        //if (StickSpawner.stickSpawner != null)
        //{
        //    SpeedForce = StickSpawner.stickSpawner.Speed;
        //}
        _blade = FindObjectOfType<BladeCollisions>();
    }

    void Update()
    {
        CheckForChild();

        if (SpeedForce >= 0.5f)
        {
            SpeedForce = 0.5f;
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
            if (StickSpawner.stickSpawner != null)
            {
                StickSpawner.stickSpawner.HasWaveEnded = true;
            }
            //_blade.Lives--;
            float YPos = -4.13f;
            float XOffset = 0.55345f;
            _obj = Instantiate(_blade.Cross, new Vector2(transform.position.x + XOffset, YPos), Quaternion.identity);
            StartCoroutine(TakeAwayObject(_obj));
            Destroy(gameObject);
        }
    }

    IEnumerator TakeAwayObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj.gameObject);
    }
}
