using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public static StickSpawner stickSpawner;

    public GameObject StickPrefab;
    public Transform[] SpawnPoints;
    public float _searchCountDown = 1f;
    private float increaseDifficultyTime = 50f;
    public float increaseDifficulty = 0.01f;
    public bool HasWaveEnded = false;
    [HideInInspector]
    public float Speed;

    private System.DateTime startTime;
    private GameManager _manager;
    public float spawnWait = 0.5f;

    void Awake()
    {
        stickSpawner = this;
        HasWaveEnded = true;
        _manager = FindObjectOfType<GameManager>();

    }

    void Start()
    {
        startTime = System.DateTime.Now;
        Speed = 0.024f;
    }

    void Update()
    {
        if (_manager._gameOver)
            Destroy(gameObject);
    }

    public void SpawnStick()
    {
        if (SticksEnabled())
        {
            Debug.Log("Spawned Stick");
            StartCoroutine(SpawnSticks());
        }

        else
            return;
    }

    bool SticksEnabled()
    {
        _searchCountDown -= Time.deltaTime;

        if (_searchCountDown <= 0)
        {
            _searchCountDown = 1;

            bool SticksAreNull = (GameObject.FindGameObjectWithTag("Stick") == null);
            if (SticksAreNull)
            {
                HasWaveEnded = true;
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnSticks()
    {
        while (true)
        {
            if (_manager._gameOver)
            {
                break;
            }

            float delay = 5;
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, SpawnPoints.Length);
            Transform spawnPoint = SpawnPoints[spawnIndex];
            float x = Random.Range(-0.767f, 0.767f);
            float y = 7.18f;
            Vector2 spawnPosition = new Vector2(x, y);

            if (transform.childCount < 2)
            {
                Instantiate(StickPrefab, spawnPosition, Quaternion.identity, parent: transform);
            }

            TryMakeGameHarder();
            yield break;
        }
    }

    private void TryMakeGameHarder()
    {
        int timePassed = (System.DateTime.Now - startTime).Seconds;

        bool makeGameHarder = timePassed > increaseDifficultyTime;
        Stick stick = FindObjectOfType<Stick>();
        if (makeGameHarder)
        {
            stick.SpeedForce += increaseDifficulty;
            Speed = stick.SpeedForce;
            startTime = System.DateTime.Now;
        }
    }
}
