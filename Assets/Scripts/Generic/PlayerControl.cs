using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{     
    public PlayerIDs MyID;

    [HideInInspector]
    public string hStr, vStr, hrStr, F1, F2, F3;

    public NavMeshAgent agent;

    [Space]
    public GameObject modelOne;
    public GameObject modelTwo;
    public GameObject modelThree;

    public Health health;

    private void OnEnable()
    {
        health.OnDeath += OnDeath;
    }

    private void OnDisable()
    {

        health.OnDeath -= OnDeath;        
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        switch (MyID)
        {
            case PlayerIDs.P1:
                hStr = "LHorizontalP1";
                vStr = "LVerticalP1";
                hrStr = "RHorizontalP1";
                F1 = "Fire1P1";
                F2 = "Fire2P1";
                F3 = "Fire3P1";
                SetCharacter(GameManager.PlayerOneIndex);
                break;
            case PlayerIDs.P2:
                hStr = "LHorizontalP2";
                vStr = "LVerticalP2";
                hrStr = "RHorizontalP2";
                F1 = "Fire1P2";
                F2 = "Fire2P2";
                F3 = "Fire3P2";
                SetCharacter(GameManager.PlayerTwoIndex);
                break;
        }

        GameManager.SetPlayerAlive(MyID, true);
    }

    private void SetCharacter(int index)
    {
        modelOne.SetActive(index == 0);
        modelTwo.SetActive(index == 1);
        modelThree.SetActive(index == 2);
    }

    private void OnDeath()
    {
        GameManager.SetPlayerAlive(MyID, false);
    }
        
}

public enum PlayerIDs
{
    P1, P2
}