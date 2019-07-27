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
        PlayerControl player = GetComponent<PlayerControl>();

        if (enemyStatesScript.detectedDis > enemyStatesScript.maxDetectDistance)
        {
            enemyStatesScript.players.Remove(player);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerControl player = other.GetComponent<PlayerControl>();
        if (player != null)
        {
            enemyStatesScript.SetStateToAlert();
            enemyStatesScript.player = player.transform;
            enemyStatesScript.players.Add(player);

            return;
        }

    }
       

}
