using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveScriptUpdated : MonoBehaviour
{
    NavMeshAgent agent;
    private Rigidbody rig;
    public float rotationSpeed = 120f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rig = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        float HCAxis = Input.GetAxis("HorizontalController");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * agent.speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);

        transform.Rotate(0,HCAxis * rotationSpeed * Time.deltaTime, 0);

        Debug.Log(HCAxis);

    }
}
