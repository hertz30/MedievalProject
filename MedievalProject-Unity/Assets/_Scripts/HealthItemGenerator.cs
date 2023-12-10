using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemGenerator : MonoBehaviour
{
    public GameObject healthItemPrefab;
    public int numberOfHealthItems = 10;
    public Vector3 spawnAreaSize = new Vector3(50f, 0f, 50f);
    public float spawnHeightOffset = 1f; // Offset for spawning higher

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        // Draw a wire cube representing the spawn area
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    void Start()
    {
        GenerateHealthItems();
    }

    void GenerateHealthItems()
    {
        for (int i = 0; i < numberOfHealthItems; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            // Instantiate the single health item prefab as a child of the calling object
            GameObject newHealthItem = Instantiate(healthItemPrefab, randomPosition, Quaternion.identity, transform);
            newHealthItem.transform.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));
            newHealthItem.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float z = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

        // Use a raycast to find the ground level at the chosen XZ position
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, transform.position.y + 10f, z), Vector3.down, out hit, 20f, LayerMask.GetMask("Ground")))
        {
            // Return the hit point with an added offset for spawning higher
            return hit.point + Vector3.up * spawnHeightOffset;
        }

        // If the raycast doesn't hit anything, default to a height of 1f plus the offset
        return new Vector3(x, 1f + spawnHeightOffset, z) + transform.position;
    }
}
