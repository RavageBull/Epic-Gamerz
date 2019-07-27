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

    private EnemyState myState;
	NavMeshAgent agent;

	public float patrolDistance = 10;
	public float delay = 1;
    private int getPositionAttempts = 0;

    public GameObject detectionCol;
    public Transform player; // The target position for the player
    [System.NonSerialized] public float detectedDis; // The distance detected between the enemy and the player
    public float maxDetectDistance = 30f; // The maximum distance the enemy can detect the player
    public float attackingDis = 5f; // The distance in which the enemy can deal damage to the player
    private float coolDownTimer;
    public float attackCoolDown = 5f; // How long until the enemy can attack again

    public List <PlayerControl> players;
    private PlayerControl attackTarget;
    private EnemyStats enemyStats;

    private PlayerControl randPlayer;

    [System.NonSerialized] public bool desCoroutineStarted = false;
    [System.NonSerialized] public bool canAttack = false;

    void Start()  
    {
        agent = GetComponent<NavMeshAgent>();
        //playerMovement = FindObjectOfType<PlayerMovement>(); // Reference to player movement script

        myState = EnemyState.Patrolling;
                             
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

                desCoroutineStarted = false;
                               
                Die();
               
                break;

            case EnemyState.Attacking:

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
        if (players.Count == 0)
        {
            SetStateToPatol();
            return;
        }

        //Calculates the distance between the enemy and a random detected player from list of players
        int randIndex = Random.Range(0, players.Count);
        randPlayer = players[randIndex];
        detectedDis = Vector3.Distance(transform.position, randPlayer.transform.position);
                      
        if(detectedDis < maxDetectDistance && detectedDis > attackingDis) // if the player is in range the chase player
        {
            attackTarget = randPlayer;
            agent.SetDestination(attackTarget.transform.position);

        }            
        else if (detectedDis <= attackingDis)
        {

            attackTarget = randPlayer;         
            SetStateToAttack();
  
        }
        else if (detectedDis > maxDetectDistance)
        {
           
            SetStateToPatol();
            return;
    
        }
             
    }

    private void AttackPlayer()
    {
        if(randPlayer != null)
        {
          detectedDis = Vector3.Distance(transform.position, randPlayer.transform.position);
        }

        if (detectedDis <= attackingDis)
        {
            agent.SetDestination(attackTarget.transform.position);

            if (coolDownTimer <= 0)
            {
                canAttack = true;
                coolDownTimer = attackCoolDown;
                Debug.Log(coolDownTimer);
            }
            else
            {
                coolDownTimer -= Time.deltaTime;
            }

            if (canAttack && players.Count > 0)
            {
                attackTarget.GetComponent<Health>().Change(-10);
                canAttack = false;
            }

        }
        else if (detectedDis > attackingDis && detectedDis < maxDetectDistance)
        {
            SetStateToAlert();
            return;
        }

        else
        {
            SetStateToPatol();
            return;
        }

    }
       
         

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
                position = GetRandomPosition(transform.position, patrolDistance);

                NavMeshHit hit;

                while (!NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas)) //checking whether the position is on a NavMesh
                {
                    position = GetRandomPosition(transform.position, patrolDistance);
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
