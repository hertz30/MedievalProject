using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;
    public Vector3 spawnAreaSize = new Vector3(50f, 0f, 50f);
    public float maxSpawnHeight = 10f;

    void Start()
    {
        GenerateEnemies();
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition();
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Instantiate the enemy prefab at the random position with random rotation
            GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, randomRotation);

            // Set the parent of the new enemy to the object calling this script
            newEnemy.transform.parent = transform;

            // Optionally, adjust other enemy properties here
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = Vector3.zero;

        // Attempt to find a random position on the NavMesh
        NavMeshHit navMeshHit;
        int attempts = 0;
        while (attempts < 30) // Try a limited number of attempts
        {
            float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f) + transform.position.x;
            float z = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f) + transform.position.z;

            if (NavMesh.SamplePosition(new Vector3(x, transform.position.y + 10f, z), out navMeshHit, 30f, NavMesh.AllAreas))
            {
                // Check if the height is within the allowed range
                if (navMeshHit.position.y < transform.position.y + maxSpawnHeight)
                {
                    randomPosition = navMeshHit.position;
                    break; // Exit the loop if a valid position is found
                }
            }

            attempts++;
        }

        return randomPosition;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wireframe cube gizmo to visualize the spawn area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}