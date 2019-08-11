using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLevelOne()
    {        
        SceneManager.LoadScene("Level 1");
    }

    public void PlayLevelTwo()
    {      
        SceneManager.LoadScene("Level 2");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public static void LoadLevelComplete()
    {
        SceneManager.LoadScene("LevelComplete");
    }

    public static void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
