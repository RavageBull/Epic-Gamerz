using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
   
    public int playerNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    
    void FixedUpdate()
    {
       
        Vector3 moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        transform.position += moveDirection * agent.speed * Time.fixedDeltaTime;

               
    }
}
