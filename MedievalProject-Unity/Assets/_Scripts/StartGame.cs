using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject aboutPage;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private GameObject creditsPage;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        titleScreen.SetActive(true);
        aboutPage.SetActive(false);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
    }

    public void PlayGame() // upon hitting the play button: ~TW
    {
        SceneManager.LoadScene("Game"); // load the first level. ~TW
        Time.timeScale = 1f; // make sure time is unfrozen incase it was before. ~TW
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void AboutPage() // switch to instructions ~TW
    {
        titleScreen.SetActive(false);
        aboutPage.SetActive(true);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
    }
    public void SettingsPage() // switch to settings ~TW
    {
        titleScreen.SetActive(false);
        aboutPage.SetActive(false);
        settingsPage.SetActive(true);
        creditsPage.SetActive(false);
    }
    public void CreditsPage() // switch to settings ~TW
    {
        titleScreen.SetActive(false);
        aboutPage.SetActive(false);
        settingsPage.SetActive(false);
        creditsPage.SetActive(true);
    }
    public void BackToMain() // switch to main ~TW
    {
        titleScreen.SetActive(true);
        aboutPage.SetActive(false);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
    }
}
