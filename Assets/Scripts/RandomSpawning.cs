using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    //Prefab and spawn point arrays
    public GameObject[] enemyPrefabs;
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    public Transform playerTransform;

    //Number of enemies and obstacles to spawn
    public int amountOfEnemies = 10;
    public int amountOfObstacles = 5;

    void Start()
    {
        //Create a modifiable list of available spawn points
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        //Seperates enemy prefabs into categories
        List<GameObject> Ammo_SkeletonPrefabs = new List<GameObject>();
        List<GameObject> EnemyPrefab = new List<GameObject>();

        foreach (GameObject enemy in enemyPrefabs)
        {
            if (enemy.CompareTag("Ammo_Pickup"))
                Ammo_SkeletonPrefabs.Add(enemy);
            else
                EnemyPrefab.Add(enemy);
        }

        //Defining how many ammo skeletons should spawn
        int ammoToSpawn = 2;
        if (ammoToSpawn > 0)

        {
            //Spawn Ammo Skeletons
            SpawnFromPoints(Ammo_SkeletonPrefabs.ToArray(), ammoToSpawn, availablePoints, true);
        }
        else
        {
            Debug.LogWarning("No Skeletons found in enemyPrefabs");
        }


        //Spawn the remaining number of regular enemies
        int regularCount = Mathf.Max(0, amountOfEnemies - ammoToSpawn);
        if (enemyPrefabs.Count() > 0)
        {
            SpawnFromPoints(EnemyPrefab.ToArray(), regularCount, availablePoints, true);
        }
        else
        {
            Debug.LogWarning("No regular enemies found to call and spawn");
        }

        //Spawn obstacles
        if (obstaclePrefabs.Length > 0)
        {
            SpawnFromPoints(obstaclePrefabs, amountOfObstacles, availablePoints, false);
        }
        else
        {
            Debug.LogWarning("No Obstacles assigned");
        }

        //Set target count in GameManager based on current regular enemies within the scene
        int realEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        FindObjectOfType<GameManager>().targetCount = realEnemyCount;
    }

    //Spawns a given number of objects from a list of prefabs at random spawn points
    void SpawnFromPoints(GameObject[] prefabs, int count, List<Transform> availablePoints, bool facePlayer)
    {
        if (prefabs == null || prefabs.Length == 0 || availablePoints.Count == 0) return;

        for (int i = 0; i < count && availablePoints.Count > 0; i++)
        {
            //Select at random a spawn point and remove from list to avoid reuse
            int spawnIndex = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[spawnIndex];
            availablePoints.RemoveAt(spawnIndex);

            //Instantiate a random prefab
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            GameObject instance = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            //Rotate prefab to face player
            if (facePlayer && playerTransform != null)
            {
                Vector3 lookDirection = playerTransform.position - instance.transform.position;
                lookDirection.y = 0;
                if (lookDirection != Vector3.zero)
                    instance.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}
