using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveScriptUpdated : MonoBehaviour
{
    NavMeshAgent agent;
    //private Rigidbody rig;
    public float rotationSpeed = 120f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rig = GetComponent<Rigidbody>();
    }


    void Update()
    {
       float hAxis = Input.GetAxis("Horizontal");
       float vAxis = Input.GetAxis("Vertical");
       float HCAxis = Input.GetAxis("HorizontalRotation");
       //float HCVAxis = Input.GetAxis("VerticalController");
        
        
        Vector3 movement = new Vector3(hAxis, 0, vAxis) * agent.speed * Time.deltaTime;
        transform.position += movement;
      // rig.MovePosition(transform.position + movement);

       transform.Rotate(0,HCAxis * rotationSpeed * Time.deltaTime, 0);

     //  Debug.Log(HCAxis);


      








        //if (Input.GetButton("Fire1"))
        //{
        //    print("Test");
        //}
        //if (Input.GetButton("Fire2"))
        //{
        //    print("This is controller 2");
        //}
    }







    /// float hAxis = Input.GetAxis("Horizontal");
    /// float vAxis = Input.GetAxis("Vertical");
    /// float HCAxis = Input.GetAxis("HorizontalController");
    /// Add another input for rotating your right stick
    /// float HCVAxis = Input.GetAxis("VerticalController");

    /// Rework this to be more in line with what you need:
    /// Vector3 moveDirection = new Vector3(hAxis, 0, vAxis);
    /// Vector3 movement = new Vector3(hAxis, 0, vAxis) * agent.speed * Time.deltaTime;
    /// Add a lookDir variable to use
    /// Vector3 lookDirection = new Vector3(HCAxis, 0, HCVAxis);

    /// transform.rotation = Quaternion.Euler(lookDirection);
		/// Move after rotation instead of before, this is causing your issues
		/// rig.MovePosition(moveDirection); 
		/// Can use moveDirection * speed instead;
        /// rig.MovePosition(transform.position + movement);

        /// transform.Rotate(0,HCAxis* rotationSpeed * Time.deltaTime, 0);

        /// Debug.Log(HCAxis);
}
