using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    private EnemyStates enemyStatesScript;
    
    void Start()
    {
        enemyStatesScript = transform.root.GetComponent<EnemyStates>(); // Reference to enemy state script
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player entered collider");
        //if (other.CompareTag("Player"))
        //{
        //    enemyStatesScript.playerDetected = true;
        //}

        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // enemyStatesScript.SetStateToAlert();
            enemyStatesScript.playerDetected = true;
            enemyStatesScript.player = player.transform;
            return;
        }

        MoveScriptUpdated playerTwo = other.GetComponent<MoveScriptUpdated>();
        if(playerTwo != null)
        {
            enemyStatesScript.playerDetected = true;
            enemyStatesScript.player = playerTwo.transform;
        }

    }

    void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {

            enemyStatesScript.playerDetected = false;            
            return;

            //if (enemyStatesScript.tarCoroutineStarted == false)
            //{
            //    enemyStatesScript.SetStateToPatol();
            //}
        }

        MoveScriptUpdated playerTwo = other.GetComponent<MoveScriptUpdated>();
        if (playerTwo != null)
        {
            enemyStatesScript.playerDetected = true;
        }
    }

}
