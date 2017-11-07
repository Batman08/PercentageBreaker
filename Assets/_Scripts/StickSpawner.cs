using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public GameObject StickPrefab;
    public Transform[] SpawnPoints;
    public float minDelay = 0.1f;
    public float manDelay = 1f;

    void Start()
    {
        StartCoroutine(SpawnSticks());
    }

    IEnumerator SpawnSticks()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, manDelay);
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, SpawnPoints.Length);
            Transform spawnPoint = SpawnPoints[spawnIndex];

            Instantiate(StickPrefab, spawnPoint.position, spawnPoint.rotation, parent: transform);
        }
    }
}
