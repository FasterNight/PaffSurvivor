using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public List<GameObject> enemyPrefabs;
    public float spawnDistanceFromView = 5f;
    public float spawnInterval = 1f;
    public int baseEnemiesPerWave = 5;

    private int currentWave = 1;
    private float mapRadius = 100f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        StartCoroutine(InitAndSpawn());
    }

    IEnumerator InitAndSpawn()
    {
        while (GameManager.Instance == null || GameManager.Instance.playerInstance == null)
            yield return null;

        player = GameManager.Instance.playerInstance.transform;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave());

            yield return new WaitUntil(() => activeEnemies.Count == 0);

            currentWave++;
        }
    }

    IEnumerator SpawnWave()
    {
        int enemiesToSpawn = baseEnemiesPerWave + (currentWave - 1) * 2;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector3 spawnPos = GetOffscreenPosition();
        spawnPos.y = 1f;

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeEnemies.Add(enemy);

        EnemyDeathNotifier deathNotifier = enemy.AddComponent<EnemyDeathNotifier>();
        deathNotifier.spawner = this;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);
    }

    Vector3 GetOffscreenPosition()
    {
        Vector3 randomDir = Random.onUnitSphere;
        randomDir.y = 0;
        randomDir.Normalize();

        Vector3 basePos = player.position + randomDir * (spawnDistanceFromView);
        basePos.x = Mathf.Clamp(basePos.x, -mapRadius, mapRadius);
        basePos.z = Mathf.Clamp(basePos.z, -mapRadius, mapRadius);
        basePos.y = 0;

        return basePos;
    }

  
}
