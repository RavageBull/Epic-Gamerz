using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    //public float speed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
                       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.back * agent.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.forward * agent.speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.left * agent.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.right * agent.speed * Time.deltaTime;
        }
        
    }
}
