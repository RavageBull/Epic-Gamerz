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
	NavMeshAgent agent;

	public float distance = 10;
	public float delay = 1;

	void Start()
    {
        myState = EnemyState.Patrolling;

		agent = GetComponent<NavMeshAgent>();

		StartCoroutine(FindDestination());
	}

	void Update()
	{

		if (Input.GetKeyDown("z"))
		{
			if (myState == EnemyState.Dead)
			{
				myState = EnemyState.Patrolling;
			}
			else
			{
				myState = EnemyState.Dead;
			}
		
		}

		switch (myState)
		{

			case EnemyState.Patrolling:


				GetComponent<MeshRenderer>().material.color = Color.green;
				
				break;

			case EnemyState.Dead:

				GetComponent<MeshRenderer>().material.color = Color.red;
				agent.SetDestination(transform.position);

				break;
		}

	}
	

	public int getPositionAttempts = 0;
	IEnumerator FindDestination()
	{
		Vector3 position = transform.position;
		while (myState == EnemyState.Patrolling) //runs while enemy is in patrolling state
		{
			if (Vector3.Distance(transform.position, position) < 1) //If distance is less then one from the chosen position, then continue with code and find a new position
			{
				position = GetRandomPosition(transform.position, distance);
				NavMeshHit hit;
				
				while (!NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas)) //checking whether the position is on a NavMesh
				{
					position = GetRandomPosition(transform.position, distance);
					getPositionAttempts++;
					yield return 0; // if this part doesn't work, then it waits one frame and tries again
				}
				getPositionAttempts = 0;
				agent.SetDestination(position);
			}
			
			yield return new WaitForSeconds(delay); //the loop will run through, wait a delay and go again
		}
	}

	Vector3 GetRandomPosition(Vector3 pos, float dist)
	{

		float angleLeft = Mathf.Clamp(-20f + (getPositionAttempts * -2f), -20, -180);
		Debug.Log("Left angle is " + angleLeft);
		float angleRight = Mathf.Clamp(20f + (getPositionAttempts * 2f), 20, 180);
		Debug.Log("Right angle is " + angleRight);


		Debug.DrawLine(pos, pos + Quaternion.Euler(0, angleLeft, 0) * transform.rotation * transform.forward * dist, Color.red);
		Debug.DrawLine(pos, pos + Quaternion.Euler(0, angleRight, 0) * transform.rotation * transform.forward * dist, Color.red);

		Quaternion anglePos = Quaternion.Euler(0, Random.Range(angleLeft, angleRight), 0) * transform.rotation;
		Vector3 position = pos + anglePos * transform.forward * dist;

		//(transform.rotation * Quaternion.Euler(0, Random.Range(-45, 45), 0)))*Vector3.forward;
		//pos + Random.insideUnitSphere * dist; //setting a random position, within an invisible sphere around object
		Debug.Log(anglePos);
		position.y = pos.y;
		return position;
	}
			
}
