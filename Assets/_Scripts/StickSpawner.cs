using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
    public GameObject StickPrefab;
    public Transform[] SpawnPoints;
    public float _searchCountDown = 1f;

    private GameManager _manager;

    void Start()
    {
        _manager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (_manager._gameOver)
            Destroy(gameObject);

        if (SticksEnabled())
            StartCoroutine(SpawnSticks());

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
                return false;
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

            float delay = 1;
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, SpawnPoints.Length);
            Transform spawnPoint = SpawnPoints[spawnIndex];
            float x = Random.Range(-2.869f, 1.842f);
            float y = 5.96f;
            Vector2 spawnPosition = new Vector2(x, y);

            if (transform.childCount < 2)
            {
                Instantiate(StickPrefab, spawnPosition, Quaternion.identity, parent: transform);
            }
            yield break;
        }
    }
}
