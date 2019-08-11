using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static int PlayerOneIndex { get; private set; }
    public static int PlayerTwoIndex { get; private set; }

    public static bool PlayerOneAlive { get; private set; }
    public static bool PlayerTwoAlive { get; private set; }
           
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

    public static void SetPlayerAlive(PlayerIDs player, bool state)
    {
        switch (player)
        {
            case PlayerIDs.P1:
                PlayerOneAlive = state;
                break;
            case PlayerIDs.P2:
                PlayerTwoAlive = state;
                break;
        }

        if(!PlayerOneAlive && !PlayerTwoAlive)
        {
            GameOver();
        }
    }

    private static void GameOver()
    {
        Debug.Log("Game Over");
        MainMenu.LoadGameOver();
    }
}
