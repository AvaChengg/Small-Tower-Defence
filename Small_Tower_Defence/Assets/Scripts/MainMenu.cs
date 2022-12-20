using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string Level;
    
    public GameObject creditsScreen;
    public GameObject settingsScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(Level);
    }
      
    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);        
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
