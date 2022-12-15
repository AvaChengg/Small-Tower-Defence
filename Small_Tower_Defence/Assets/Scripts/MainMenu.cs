using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //public bool isStart;
    //public bool isQuit;
    //
    //void OnMouseUp()
    //{
    //    if(isStart)
    //    {
    //        Application.LoadLevel(1);
    //    }
    //    if (isQuit)
    //    {
    //        Application.Quit();
    //    }
    //}

    public string Level;

    public void StartGame()
    {
        SceneManager.LoadScene(Level);
    }

    public void OpenSettings()
    {

    }

    public void CloseOptions()
    {

    }

    public void OpenCredits()
    {

    }

    public void CloseCredits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
