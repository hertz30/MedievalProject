using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if all gem values are 1
            if (PlayerPrefs.GetInt("Collected_ElfGem") == 1 &&
                PlayerPrefs.GetInt("Collected_GoblinGem") == 1 &&
                PlayerPrefs.GetInt("Collected_HumanGem") == 1 &&
                PlayerPrefs.GetInt("Collected_UndeadGem") == 1)
            {
                // All gem values are 1, end the game and switch to a different scene
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        // Reset condition variables to 0
        ResetConditionVariables();

        // Switch to the end game scene
        SceneManager.LoadScene("EndScreen");
    }

    private void ResetConditionVariables()
    {
        // Set all gem values to 0
        PlayerPrefs.SetInt("Collected_ElfGem", 0);
        PlayerPrefs.SetInt("Collected_GoblinGem", 0);
        PlayerPrefs.SetInt("Collected_HumanGem", 0);
        PlayerPrefs.SetInt("Collected_UndeadGem", 0);

        // Save the changes
        PlayerPrefs.Save();
    }
}