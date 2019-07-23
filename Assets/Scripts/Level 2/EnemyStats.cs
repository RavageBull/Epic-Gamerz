using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private EnemyStates enemyStatesScript;

    public float health = 100;
    public float damage = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyStatesScript = transform.GetComponent<EnemyStates>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health < 0)
        {
            enemyStatesScript.SetStateToDead();
        }

    }
}
