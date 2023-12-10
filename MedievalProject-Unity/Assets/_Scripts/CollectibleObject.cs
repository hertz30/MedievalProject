using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    // Define a unique identifier for each collectible type
    public string collectibleType = "Default";

    public static float rotationSpeed = 30f;
    public static float bobbingHeight = 0.4f;
    public static float bobbingSpeed = .5f;

    private float startY;

    void Start()
    {
        // Store the initial Y position for reference
        startY = transform.position.y;

        // Start the bobbing motion coroutine
        StartCoroutine(BobbingMotion());
    }

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

    IEnumerator BobbingMotion()
    {
        while (true)
        {
            // Calculate vertical offset using a sine function for oscillation
            float yOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

            // Update the position of the collectible object
            transform.position = new Vector3(transform.position.x, startY + yOffset, transform.position.z);

            yield return null;
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