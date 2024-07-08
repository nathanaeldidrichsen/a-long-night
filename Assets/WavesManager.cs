using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform spawnTransform;
        public GameObject enemyPrefab;
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public float spawnInterval = 3f;
    public int enemiesPerWave = 5;
    public float waveRestartTime = 5f;

    private List<GameObject> currentEnemies = new List<GameObject>();
    private bool isWaitingForWave = false;

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        isWaitingForWave = false;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                GameObject enemy = Instantiate(spawnPoint.enemyPrefab, spawnPoint.spawnTransform.position, Quaternion.identity);
                currentEnemies.Add(enemy);
            }
            yield return new WaitForSeconds(spawnInterval);
        }

        // Check if all enemies are null
        yield return new WaitForSeconds(1f); // Give time for enemies to potentially destroy themselves

        bool allEnemiesNull = true;
        foreach (GameObject enemy in currentEnemies)
        {
            if (enemy != null)
            {
                allEnemiesNull = false;
                break;
            }
        }

        if (allEnemiesNull)
        {
            StartCoroutine(RestartWave());
        }
        else
        {
            isWaitingForWave = true; // Start waiting for the wave to finish
        }
    }

    IEnumerator RestartWave()
    {
        yield return new WaitForSeconds(waveRestartTime);
        StartWave();
    }

    void Update()
    {
        // Example: Trigger wave restart manually (for testing purposes)
        if (Input.GetKeyDown(KeyCode.R) && isWaitingForWave)
        {
            StopAllCoroutines(); // Stop the current wave and restart
            StartWave();
        }
    }
}
