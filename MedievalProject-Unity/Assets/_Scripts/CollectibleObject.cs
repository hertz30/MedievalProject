using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    // Define a unique identifier for each collectible type
    public string collectibleType = "Default";

    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotate the item around its local up axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Check if the corresponding PlayerPrefs variable is set to 1 (true)
        if (PlayerPrefs.GetInt("Collected_" + collectibleType) == 1)
        {
            // Destroy the collectible object
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger collider
        if (other.CompareTag("Player"))
        {
            // Set the PlayerPrefs variable to true (collected)
            PlayerPrefs.SetInt("Collected_" + collectibleType, 1);
        }
    }
}