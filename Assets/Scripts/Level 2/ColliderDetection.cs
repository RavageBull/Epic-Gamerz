using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    EnemyStates enemyStatesScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player 1") || other.CompareTag("Player 2"))
        {
            enemyStatesScript.playerDetected = true;
        }
    }
        
}
