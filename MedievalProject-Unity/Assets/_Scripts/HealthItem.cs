using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public float healthRestore = 0.5f; // Adjust the amount of health to restore as needed
    private static GameObject player;
    public float rotationSpeed = 30f;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        // Rotate the item around its local up axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger collider
        if (other.CompareTag("Player"))
        {
            // Restore health to the player
            player.GetComponent<PlayerCC>().health = Mathf.Min(player.GetComponent<PlayerCC>().health + healthRestore, 1);
            // Destroy the health item
            Destroy(gameObject);
        }
    }
}