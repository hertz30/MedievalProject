using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Array of cloud prefabs
    public int numberOfClouds = 10;
    public Vector3 spawnAreaSize = new Vector3(50f, 0f, 50f);

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        // Draw a wire cube representing the spawn area
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }

    void Start()
    {
        GenerateClouds();
    }

    void GenerateClouds()
    {
        for (int i = 0; i < numberOfClouds; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            // Randomly choose a cloud prefab from the array
            GameObject selectedCloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

            // Instantiate the selected cloud prefab as a child of the CloudGenerator
            GameObject newCloud = Instantiate(selectedCloudPrefab, randomPosition, Quaternion.identity, transform);

            // You can add additional customization based on your cloud prefab
            // For example, you might want to randomize the scale or rotation
            newCloud.transform.localScale = new Vector3(Random.Range(1f, 3f), Random.Range(1f, 3f), Random.Range(1f, 3f));
            newCloud.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float z = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);
        float y = Random.Range(15f, 20f); // Adjust the height as needed

        return new Vector3(x, y, z) + transform.position;
    }
}