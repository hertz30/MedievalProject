using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateKillsText : MonoBehaviour
{
    void Start()
    {
        // Iterate through all child objects
        foreach (Transform child in transform)
        {
            // Check if the child has a Text component
            Text killsText = child.GetComponent<Text>();

            if (killsText != null)
            {
                // Retrieve the value of "EnemiesKilled" from PlayerPrefs
                int enemiesKilled = PlayerPrefs.GetInt("EnemiesKilled", 0);

                // Set the text value to "Total Kills: X" for each child with a Text component
                killsText.text = "Total Kills: " + enemiesKilled;
            }
        }
    }
}