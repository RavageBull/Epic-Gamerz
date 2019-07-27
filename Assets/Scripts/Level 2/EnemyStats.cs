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

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health < 0)
        {
            Debug.Log("I is dead");
            enemyStatesScript.SetStateToDead();
        }

    }
}
//public float radius = 0.5f;
//Debug.DrawLine(firePoint.position, ray.GetPoint(maxDistance), Color.red, 5f);
//if (Physics.SphereCast(ray, radius, out hit, maxDistance))