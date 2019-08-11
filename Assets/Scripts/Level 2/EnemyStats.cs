using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private EnemyStates enemyStatesScript;

    public float health = 100;
    public int damage = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStatesScript = transform.GetComponent<EnemyStates>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health < 0)
        {
          //  Debug.Log("I is dead");
            enemyStatesScript.SetStateToDead();
        }

    }
}
