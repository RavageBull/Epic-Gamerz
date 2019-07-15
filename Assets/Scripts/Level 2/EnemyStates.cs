using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Alert,
        Dead,
        Attacking
    }

    public EnemyState myState;
	NavMeshAgent agent;

	public float distance = 10;
	public float delay = 1;
    public int getPositionAttempts = 0;

    public GameObject detectionCol;
    //public bool playerDetected = false;
    public Transform player; // The target position for the player
    private float detectedDis;
    public float maxDetectDistance = 30f;
    private float attackingDis = 10f;

    public List<PlayerMovement> players;
    private PlayerMovement attackTarget;
    private EnemyStats enemyStats;
    
    public bool desCoroutineStarted = false;
    
    void Start()  
    {
        agent = GetComponent<NavMeshAgent>();
        //playerMovement = FindObjectOfType<PlayerMovement>(); // Reference to player movement script

        myState = EnemyState.Patrolling;

        if (!desCoroutineStarted)
        {
            StartCoroutine(FindDestination());
        }
                
    }

	void Update()
    {
        HandleStates();
                
	}

    public void SetStateToAlert()
    {
        EnterState(EnemyState.Alert);
    }

    public void SetStateToPatol()
    {
        EnterState(EnemyState.Patrolling);
    }

    public void SetStateToDead()
    {
        EnterState(EnemyState.Dead);
    }

    public void SetStateToAttack()
    {
        EnterState(EnemyState.Attacking);
    }

    private void EnterState(EnemyState state)
    {
        myState = state;
    }

    private void HandleStates()
    {
        switch (myState)
        {

            case EnemyState.Patrolling:

                GetComponent<MeshRenderer>().material.color = Color.blue;

                if (!desCoroutineStarted)
                {
                    StartCoroutine(FindDestination());
                }

                break;

            case EnemyState.Alert:

                GetComponent<MeshRenderer>().material.color = Color.green;

                desCoroutineStarted = false;

                TargetPlayer();
                               
                break;

            case EnemyState.Dead:
                               
                Die();

                desCoroutineStarted = false;
               
                break;

            case EnemyState.Attacking:

                Debug.Log("I should be " + myState);
                GetComponent<MeshRenderer>().material.color = Color.red;

                desCoroutineStarted = false;

                AttackPlayer();

                break;
        }
    }
    
    private void SwitchStates()
    {
        if (Input.GetKeyDown("z"))
        {
            if (myState == EnemyState.Dead)
            {
                myState = EnemyState.Patrolling;
            }

            else if (myState == EnemyState.Patrolling)
            {
                myState = EnemyState.Alert;
            }

            else
            {
                myState = EnemyState.Dead;
            }

        }
    }
      
    private void TargetPlayer()
    {
        if(players.Count == 0)
        {
            SetStateToPatol();
            return;
        }
               
        //Calculates the distance between the enemy and a random detected player from list of players
        int randIndex = Random.Range(0, players.Count);
        PlayerMovement randPlayer = players[randIndex];
        detectedDis = Vector3.Distance(transform.position, randPlayer.transform.position);
                        

        if(detectedDis < maxDetectDistance) // if the player is in range the chase player
        {

            attackTarget = randPlayer;
            agent.SetDestination(randPlayer.transform.position);

            if (detectedDis < attackingDis)
            {
                SetStateToAttack();
                Debug.Log("State set to attack" + myState);
            }
        }            
             
    }

    private void AttackPlayer()
    {

        Debug.Log("Damage dealt");
        attackTarget.TakeDamage(enemyStats.damage);
    }



        //RaycastHit hit;
virtual        
        //Debug.DrawLine(transform.position, (transform.forward * distance), Color.red, 5f);

        //if (Physics.Raycast(ray, out hit, distance))
        //{
        //    Debug.Log(hit.transform.name);

        //    if (attackTarget != null)
        //    {
        //        attackTarget.TakeDamage(enemyStats.damage);
        //        Debug.Log("Damage dealt");
        //    }

        //    //if ()
        //    //{

        //    //}
        //    // Set back to alert
        //}
    

    #region Detection

    IEnumerator FindDestination()
    {
        desCoroutineStarted = true;

        Vector3 position = transform.position;

        bool enemyIsStuck = false;

        while (myState == EnemyState.Patrolling) //runs while enemy is in patrolling state
        {
            if (Vector3.Distance(transform.position, position) < 1 || enemyIsStuck || !agent.isOnNavMesh)
            // If distance is less then one from the chosen position, and/or enemy is stuck, and/or enemy isn't on navmesh anymore
            // then continue with code and find a new position
            {
                enemyIsStuck = false;
                position = GetRandomPosition(transform.position, distance);

                NavMeshHit hit;

                while (!NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas)) //checking whether the position is on a NavMesh
                {
                    position = GetRandomPosition(transform.position, distance);
                    getPositionAttempts++;
                    yield return 0; // if this part doesn't work, then it waits one frame and tries again
                }

                // Set position to actual NavMesh point height (+1)
                position.y = (hit.position != null ? hit.position.y + 1 : position.y);
                // Reset GetPosition attempt counter
                getPositionAttempts = 0;
                // Tell the NavMesh agent to go to the position
                agent.SetDestination(position);
            }

            else if (agent.velocity == Vector3.zero)
            {
                // Enemy is stuck - make sure a new position is calculated in next round.
                // (Enemy is not nearby his target yet & he is not moving = he can't reach the target)
                enemyIsStuck = true;
            }

            Debug.DrawLine(transform.position, position, Color.cyan);
            yield return new WaitForSeconds(delay); //the loop will run through, wait a delay and go again
        }
    }

    Vector3 GetRandomPosition(Vector3 pos, float dist)
    {
        float angleChange = getPositionAttempts * 5f; //Creates variable of how much the angle size should change by getting number of attempts times 5
        int circle = (int)(angleChange / 360); //Gets the position of the angle change on circle around game object

        float angleLeft = -20f + angleChange;
        float angleRight = 20f + angleChange;


        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleLeft, Vector3.up) * (transform.forward.normalized * dist), Color.red);
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleRight, Vector3.up) * (transform.forward.normalized * dist), Color.red);

        float randomAngle = Random.Range(angleLeft, angleRight); //Uses current cone size to get a random angle
        Quaternion anglePos = transform.rotation * Quaternion.Euler(0, randomAngle, 0);
        Vector3 position = pos + Quaternion.AngleAxis(randomAngle, Vector3.up) * (transform.forward.normalized * dist);
        // position adds to the angle axis (which is given the random angle value and up axis) then times the forward direction and distance.
        //New random position is decided with an angle rotation in a cone in front of enemy.

        position.y = pos.y;
        return position;
    }

    #endregion

    private void Die()
    {
        Debug.Log("I Died");
        Destroy(gameObject);
    }
     
}
