using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    private EnemyStates enemyStatesScript;
    
    void Start()
    {
        enemyStatesScript = FindObjectOfType<EnemyStates>(); // Reference tp enemy state script
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Player entered collider");
        if (other.CompareTag("Player"))
        {
            enemyStatesScript.playerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left collider");
            enemyStatesScript.playerDetected = false;
        }
    }

}
