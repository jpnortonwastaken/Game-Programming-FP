using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;

    public float spawnTime = 3;
    public float xMin = -25;
    public float yMin = 8;
    public float zMin = -25;
    public float xMax = 25;
    public float yMax = 25;
    public float zMax = 25;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    void SpawnEnemies()
    {
        Vector3 enemyPosition;

        enemyPosition.x = Random.Range(xMin, xMax);
        enemyPosition.y = Random.Range(yMin, yMax);
        enemyPosition.z = Random.Range(zMin, zMax);
        GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation)
            as GameObject;

        spawnedEnemy.transform.parent = gameObject.transform;
    }
}

