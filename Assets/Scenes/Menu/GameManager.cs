using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static int PlayerOneIndex { get; private set; }
    public static int PlayerTwoIndex { get; private set; }

    //public static bool playersDied;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerOne(int index)
    {
        PlayerOneIndex = index;
    }

    public void SetPlayerTwo(int index)
    {
         PlayerTwoIndex = index;
    }

    public static void ResetCharacters()
    {
        PlayerOneIndex = 0;
        PlayerTwoIndex = 0;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
