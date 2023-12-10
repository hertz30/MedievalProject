using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the BoxCollider!");
            // You can add additional actions or logic here
        }
    }
}
