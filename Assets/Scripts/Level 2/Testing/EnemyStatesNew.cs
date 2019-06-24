using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatesNew : MonoBehaviour
{
    public enum EnemyState
    {
        Patrolling,
        Dead
    }

    EnemyState myState;
    public EnemyState MyState { get => myState; set {myState = value; EnemyStateChanged(); } }

    NavMeshAgent agent;

    float maxSlope = 45; // Degrees
    float navMeshMaxDistance = 2f;

    [SerializeField]
    private float distance = 10;
    public float Distance { get => distance; set { distance = value; DistanceChanged(); } }


    public float delay = 1;

    public int getPositionAttempts = 0;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Get the maxSlope from the NavMesh build settings
        NavMeshBuildSettings nmSettings = NavMesh.GetSettingsByID(agent.agentTypeID);
        maxSlope = nmSettings.agentSlope;
        DistanceChanged();

        MyState = EnemyState.Patrolling;
                        
    }

    void Update()
    {

        if (Input.GetKeyDown("z"))
        {
            if (MyState == EnemyState.Dead)
            {
                MyState = EnemyState.Patrolling;
            }
            else
            {
                MyState = EnemyState.Dead;
            }
        }

    }

    // Calculate the maximum distance from a desired patrol endpoint
    void DistanceChanged()
    {
        navMeshMaxDistance = Distance * Mathf.Tan(maxSlope * Mathf.Deg2Rad);
    }

    // This method is called when the EnemyState is changed.
    void EnemyStateChanged()
    {
        switch (MyState)
        {
            case EnemyState.Patrolling:
                GetComponent<MeshRenderer>().material.color = Color.green;
                StartCoroutine(FindDestination());
                break;
            case EnemyState.Dead:
                GetComponent<MeshRenderer>().material.color = Color.red;
                agent.SetDestination(transform.position);
                break;
        }
    }


    IEnumerator FindDestination()
    {
        Vector3 position = transform.position;

        bool enemyIsStuck = false;
        while (MyState == EnemyState.Patrolling) //runs while enemy is in patrolling state
        {
            if (Vector3.Distance(transform.position, position) < 1 || enemyIsStuck || !agent.isOnNavMesh) //If distance is less then one from the chosen position or enemy is stuck or enemy isnt on navmesh anymore, then continue with code and find a new position
            {
                enemyIsStuck = false;
                position = GetRandomPosition(transform.position, Distance);

                NavMeshHit hit;
                bool foundPath = false;
                while (!NavMesh.SamplePosition(position, out hit, 2, agent.areaMask) || !foundPath) //checking whether the position is on a NavMesh
                {

                    position = GetRandomPosition(transform.position, Distance);

                    // If Negative Infinity returned (=>Player can't move any more)
                    if (position.x == Vector3.negativeInfinity.x)
                        yield break;

                    // --- Check if distance to random position really is nearby (ATTENTION: This might be inefficient/inperformant when spawning many enemies? I'm not sure!)
                    NavMeshPath path = new NavMeshPath();
                    NavMesh.CalculatePath(transform.position, position, agent.areaMask, path);


                    // Calculate the total path distance to the random position
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        float dist = 0f;
                        for (int i = 0; i < path.corners.Length; i++)
                        {
                            if (i != path.corners.Length - 1)
                                dist += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                        }
                        if (dist <= distance)
                        {
                            foundPath = true;
                            Debug.Log("YESSSS" + dist);
                        }
                        else
                        {
                            foundPath = false;
                            Debug.Log("NOOOO" + dist);
                        }
                    }
                    // ---  
                    

                    getPositionAttempts++;
                    yield return 0; // if this part doesn't work, then it waits one frame and tries again
                }

                // Set position to actual NavMesh point height (+1)
                position.y = (hit.position != null?hit.position.y+1:position.y);

                // Reset GetPosition attempt counter
                getPositionAttempts = 0;

                // Tell the NavMesh agent to go to the position
                agent.SetDestination(position);
            }
            else if(agent.velocity == Vector3.zero)
            {
                // Enemy is stuck - make sure a new position is calculated in next round.
                // (Enemy is not nearby his target yet & he is not moving = he can't reach the target)
                enemyIsStuck = true;
            }
            
            Debug.DrawLine(transform.position, position, Color.cyan);

            yield return new WaitForSeconds(delay); //the loop will run through, wait a delay and go again
        }
    }

    
    // @Tash: Following function makes the line of sight cone bigger. Maybe needs some optimizations
    /*Vector3 GetRandomPosition(Vector3 pos, float dist)
    {
        float angleLeft = -20f + (getPositionAttempts * -2f);
        float angleRight = 20f + (getPositionAttempts * 2f);

        // Reduce distance the more failed attempts there are
        int circles = (int)(angleRight / 200);
        dist /= circles > 0? circles:1;
        if (dist <= 1)
        {// Too many attempts, this enemy is stuck forever.
            Die();
            return Vector3.negativeInfinity;
        }

        // Following 2 lines are solely for Debugging
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleLeft, Vector3.up) * (transform.forward.normalized * dist), Color.red);
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleRight, Vector3.up) * (transform.forward.normalized * dist), Color.red);

        // Get a random angle within the current sight cone
        float randomAngle = Random.Range(angleLeft, angleRight);
        Quaternion anglePos = transform.rotation * Quaternion.Euler(0, randomAngle, 0);
        Vector3 position = pos + Quaternion.AngleAxis(randomAngle, Vector3.up) * (transform.forward.normalized * dist);
        
        position.y = pos.y;
        return position;
    }*/






    
    // @Tash: Following function makes the line of sight cone go around clockwise
    Vector3 GetRandomPosition(Vector3 pos, float dist)
    {
        float angleChange = getPositionAttempts * 5f;
        int circles = (int)(angleChange / 360);

        float angleOne = -20f + angleChange;
        float angleTwo = 20f + angleChange;
        

         
        // Reduce distance the more failed attempts there are
        dist /= circles > 0? circles:1;
        if (dist <= 2)
        {// Too many attempts, this enemy is stuck forever.
            Die();
            return Vector3.negativeInfinity;
        }

        // Following 2 lines are solely for Debugging
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleOne, Vector3.up) * (transform.forward.normalized * dist), Color.red);
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleTwo, Vector3.up) * (transform.forward.normalized * dist), Color.red);

        // Get a random angle within the current sight cone
        float randomAngle = Random.Range(angleOne, angleTwo);
        Quaternion anglePos = transform.rotation * Quaternion.Euler(0, randomAngle, 0);
        Vector3 position = pos + Quaternion.AngleAxis(randomAngle, Vector3.up) * (transform.forward.normalized * dist);

        position.y = pos.y;
        return position;
    }
    
    
    // @Tash: Following function makes the line of sight cone go around BUT, it switches between left & right side after each attempt. //PS might not be perfectly working yet
    /*Vector3 GetRandomPosition(Vector3 pos, float dist)
    {
        float angleChange = getPositionAttempts * 5f;
        int circles = (int)(angleChange / 180);

        float angleOne = 0;
        float angleTwo = 0;


        if (getPositionAttempts % 2 == 0)
        {
            angleOne = -20f - angleChange;
            angleTwo = 20f - angleChange;
        }
        else
        {
            angleOne = -20f + angleChange;
            angleTwo = 20f + angleChange;
        }


        // Reduce distance the more failed attempts there are

        dist /= circles > 0 ? circles : 1;
        if (dist <= 2)
        {// Too many attempts, this enemy is stuck forever.
            Die();
            return Vector3.negativeInfinity;
        }

        // Following 2 lines are solely for Debugging
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleOne, Vector3.up) * (transform.forward.normalized * dist), Color.red);
        Debug.DrawLine(pos, pos + Quaternion.AngleAxis(angleTwo, Vector3.up) * (transform.forward.normalized * dist), Color.red);

        // Get a random angle within the current sight cone
        float randomAngle = Random.Range(angleOne, angleTwo);
        Quaternion anglePos = transform.rotation * Quaternion.Euler(0, randomAngle, 0);
        Vector3 position = pos + Quaternion.AngleAxis(randomAngle, Vector3.up) * (transform.forward.normalized * dist);

        position.y = pos.y;
        return position;
    }*/

    public void Die()
    {
        MyState = EnemyState.Dead;
    }
    

}
