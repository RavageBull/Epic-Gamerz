using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Dead
    }

    EnemyState myState;

    // Start is called before the first frame update
    void Start()
    {
        myState = EnemyState.Dead;
        if (Random.Range(0, 100) > 50)
        {
            myState = EnemyState.Patrolling;

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("z"))
        {
            if(myState == EnemyState.Dead)
            {
                myState = EnemyState.Patrolling;
            }
            else
            {
                myState = EnemyState.Dead;
            }


        }


        switch (myState) {

            case EnemyState.Patrolling:
                this.GetComponent<NavMeshAgent>().SetDestination(new Vector3(0, 0, 0));
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case EnemyState.Dead:



                this.GetComponent<MeshRenderer>().material.color = Color.red;
                this.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);








                break;
        }
    }




}
