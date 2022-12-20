using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string MainMenu;

    public GameObject settingsScreen;
    public GameObject pauseScreen;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void ClosePause()
    {
        pauseScreen.SetActive(false);
    }
}
