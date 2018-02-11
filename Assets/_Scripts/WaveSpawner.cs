using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform Stick;
        public int count;
        public float Rate;
    }

    public Wave[] Waves;
    public Transform[] SpawnPoints;
    public SpawnState State = SpawnState.Counting;
    public float TimeBetweenWaves = 5f;

    private int _nextWave = 0;
    private float _waveCountDown;
    private float _searchCountDown = 1f;

    void Start()
    {
        _waveCountDown = TimeBetweenWaves;
    }

    void Update()
    {
        CheckSpawnStates();
    }

    void CheckSpawnStates()
    {
        if (State == SpawnState.Waiting)
        {
            //Check if sticks are still enabled
            if (!SticksEnabled())
                //Start new wave
                WaveCompleted();
            else
                return;
        }

        bool WaveCountDownIsLessThanZero = (_waveCountDown <= 0);
        if (WaveCountDownIsLessThanZero)
        {
            bool StateIsNotSetToSpawning = (State != SpawnState.Spawning);
            if (StateIsNotSetToSpawning)
                //Start Spawning a Wave
                StartCoroutine(SpawnWave(Waves[_nextWave]));
        }
        else
            _waveCountDown -= Time.deltaTime;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        State = SpawnState.Counting;
        _waveCountDown = TimeBetweenWaves;

        int NextWave = _nextWave + 1;
        int MaxWaves = Waves.Length - 1;
        //Do error check to see if any waves are left
        if (NextWave > MaxWaves)
        {
            _nextWave = 0;
            Debug.Log("All waves complete, Looping.....");
        }
        else
            _nextWave++;
    }

    bool SticksEnabled()
    {
        _searchCountDown -= Time.deltaTime;

        if (_searchCountDown <= 0)
        {
            bool SticksAreNull = (GameObject.FindGameObjectWithTag("Stick") == null);
            if (SticksAreNull)
                return false;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.Name);
        State = SpawnState.Spawning;

        //Spawn Sticks
        for (int i = 0; i < wave.count; i++)
        {
            SpawnStick(wave.Stick);
            yield return new WaitForSeconds(1 / wave.Rate);
        }

        State = SpawnState.Waiting;

        yield break;
    }

    void SpawnStick(Transform Stick)
    {
        //Spawn Enemy
        Debug.Log("Spawning Stick: " + Stick.name);

        bool NoSpawnPoints = (SpawnPoints.Length == 0);
        if (NoSpawnPoints)
            Debug.LogError("No Spawn Points referenced");

        Transform SpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(Stick, SpawnPoint.position, SpawnPoint.rotation);
    }
}
