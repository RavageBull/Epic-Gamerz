using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour

{


    public enum PlayerIDs
    {
        P1, P2
    }

    public PlayerIDs MyID;

    [HideInInspector]
    public string hStr, vStr, hrStr, F1, F2, F3;

    public NavMeshAgent agent;
    
    void Awake()
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

                break;
            case PlayerIDs.P2:
                hStr = "LHorizontalP2";
                vStr = "LVerticalP2";
                hrStr = "RHorizontalP2";
                F1 = "Fire1P2";
                F2 = "Fire2P2";
                F3 = "Fire3P2";
                break;
        }
    }
}
