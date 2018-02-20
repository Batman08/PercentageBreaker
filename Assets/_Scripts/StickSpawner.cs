using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public GameObject StickPrefab;
    public Transform[] SpawnPoints;
    //public float minDelay = 0.1f;
    public float minDelay = 1f;
    public float manDelay = 2f;

    private GameManager _manager;

    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnSticks());
    }

    void Update()
    {
        if (_manager._gameOver)
            Destroy(gameObject);
    }

    IEnumerator SpawnSticks()
    {
        while (true)
        {
            if (_manager._gameOver)
            {
                break;
            }
            //float delay = Random.Range(minDelay, manDelay);
            float delay = 2;
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, SpawnPoints.Length);
            Transform spawnPoint = SpawnPoints[spawnIndex];

            Instantiate(StickPrefab, spawnPoint.position, spawnPoint.rotation, parent: transform);
        }
    }
}
