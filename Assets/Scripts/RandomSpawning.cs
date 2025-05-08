using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] obstaclePrefabs;
    public float spawnRadius = 2f;
    public float spawnInterval = 5f;

    public int amountOfEnemies = 10;
    public int amountOfObstacles = 5;
    public float rayLength = 50f;

    public Vector2 topLeft = new Vector2(-50f, 50f);
    public Vector2 bottomRight = new Vector2(50f, -50f);
    public float height = 30f;

    void Start()
    {
        SpawnGroup(enemyPrefabs, amountOfEnemies);
        SpawnGroup(new GameObject[] { obstaclePrefabs }, amountOfObstacles);
    }

    async void SpawnGroup(GameObject[] prefabs, int count) 
    {
        int spawned = 0;
        int maxAttempts = count * 10;
        int attempts = 0;

        while (spawned < count && attempts < maxAttempts) 
        { 
            attempts++;
            await Task.Delay(1);
            if (!Application.isPlaying) return;

            Vector3 spawnPoint = GetRandomPoint();
            if (TryGetSpawnPosition(spawnPoint, out Vector3 hitPoint)) 
            {
                if (!HasNearbyObjects(hitPoint)) 
                {
                    GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
                    Instantiate(prefabToSpawn, hitPoint, Quaternion.identity);
                    spawned++;
                }
            }
        }
        Vector3 GetRandomPoint() 
        {
            float x = Random.Range(topLeft)
        }
    }
}