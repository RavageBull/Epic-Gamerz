using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerPosition += Vector3.back * agent.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerPosition += Vector3.forward * agent.speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerPosition += Vector3.left * agent.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerPosition += Vector3.right * agent.speed * Time.deltaTime;
        }
        
    }
}
