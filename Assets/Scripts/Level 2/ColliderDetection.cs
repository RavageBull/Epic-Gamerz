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

    void Update()
    {
        PlayerMovement player = GetComponent<PlayerMovement>();
        if (enemyStatesScript.detectedDis > enemyStatesScript.maxDetectDistance)
        {
            enemyStatesScript.players.Remove(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            enemyStatesScript.SetStateToAlert();
            enemyStatesScript.player = player.transform;
            enemyStatesScript.players.Add(player);

            return;
        }

        MoveScript playerTwo = other.GetComponent<MoveScript>();
        if (playerTwo != null)
        {
            enemyStatesScript.SetStateToAlert();
            enemyStatesScript.player = playerTwo.transform;
            //enemyStatesScript.players2.Add(playerTwo);
        }

    }

    //void OnTriggerExit(Collider other)
    //{
    //    PlayerMovement player = other.GetComponent<PlayerMovement>();
    //    if (player != null)
    //    {
    //        if (enemyStatesScript.detectedDis > enemyStatesScript.maxDetectDistance)
    //        {
    //            enemyStatesScript.players.Remove(player);
    //        }
                
    //        return;

    //    }

    //    //MoveScriptUpdated playerTwo = other.GetComponent<MoveScriptUpdated>();
    //    //if (playerTwo != null)
    //    //{
    //    //    enemyStatesScript.SetStateToPatol();
    //    //}
    //}

}
