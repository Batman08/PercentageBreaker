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

    private readonly string RoundCountKey = "NumberOfRoundsKey";
    public int roundNumCount;

    void Awake()
    {
        stickSpawner = this;
        HasWaveEnded = true;
        _manager = FindObjectOfType<GameManager>();
        roundNumCount = 0;
        PlayerPrefs.SetInt(RoundCountKey, roundNumCount);
        PlayerPrefs.SetFloat("CurrentPosition", 0);
    }

    void Start()
    {
        startTime = System.DateTime.Now;
        Speed = 0.024f;
    }

    void Update()
    {
        bool NoStickAsChildrenOfSpawner = ((transform.childCount < 2));
        if (NoStickAsChildrenOfSpawner)
        {
            StartCoroutine(EnableBoolWaveStart());
            return;
        }

        bool GameIsOver = (_manager._gameOver);
        if (GameIsOver)
            gameObject.SetActive(value: false);
    }

    IEnumerator EnableBoolWaveStart()
    {
        HasWaveEnded = true;
        yield return new WaitForSeconds(0.5f);
        HasWaveEnded = false;
    }

    public void SpawnStick()
    {
        if (SticksEnabled())
        {
            //Debug.Log("Spawned Stick");
            StartCoroutine(SpawnSticks());
        }

        else
            return;
    }//4D4D4DFF - buttons colour hex

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

            bool PastFiveRounds = (PlayerPrefs.GetInt(RoundCountKey) >= 2);
            if (PastFiveRounds)
            {
                //_manager.ChangeBufferPercentage();
                roundNumCount = 0;
                PlayerPrefs.GetInt(RoundCountKey, roundNumCount);
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
                roundNumCount += 1;
                PlayerPrefs.SetInt(RoundCountKey, roundNumCount);
                Debug.Log(PlayerPrefs.GetInt(RoundCountKey));
                Instantiate(StickPrefab, spawnPosition, Quaternion.identity, parent: transform);
            }
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