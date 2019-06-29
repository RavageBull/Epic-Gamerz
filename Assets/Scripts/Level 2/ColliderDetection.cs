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

        Debug.Log("Triggered");
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("Detected");
            // enemyStatesScript.SetStateToAlert();
            enemyStatesScript.playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {

            enemyStatesScript.playerDetected = false;

            //if (enemyStatesScript.tarCoroutineStarted == false)
            //{
            //    enemyStatesScript.SetStateToPatol();
            //}
        }
    }

}
