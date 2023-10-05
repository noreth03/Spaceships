using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool isGameActive = true;
    public GameObject enemyPrefab; 
    public float xRange = 3f;
    public float yRange = 3f; 
    public float delay = 5f; 
     public float spawnOffset = 2f;

    
    void Start()
    {
        
        if (enemyPrefab != null)
        {
            StartCoroutine(SpawnEnemiesTimer());
        }
        else
        {
            Debug.LogError("No se ha asignado un prefab de enemigo.");
        }
    }

    IEnumerator SpawnEnemiesTimer()
    {
        while (isGameActive)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnEnemies()
{
    Vector3 cameraPosition = Camera.main.transform.position;
    Vector2 spawnPosition = new Vector2(Random.Range(-xRange, xRange) * spawnOffset, cameraPosition.y + Camera.main.orthographicSize + yRange);
    GameObject newSpawnedObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    newSpawnedObject.transform.parent = transform;
}
}

