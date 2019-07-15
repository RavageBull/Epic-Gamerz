using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
   
    public int playerNumber = 1;
    public float rotationSpeed = 100f;

    //Camera cam;
    private Health health;
 
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //cam = Camera.main;
        health = GetComponent<Health>();
    }

    private void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        float mouseX = Input.GetAxis("HorizontalRotation");
        float mouseY = Input.GetAxis("VerticalRotation");

        //float heading = Mathf.Atan2(mouseX, mouseY);
        //transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);




        /*float cameraDiff = cam.transform.position.y - transform.position.y;
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDiff));
        Vector3 turretLookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        transform.LookAt(turretLookDirection);*/
    }

    void FixedUpdate()
    {
       
        Vector3 moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        transform.position += moveDirection * agent.speed * Time.fixedDeltaTime;

        //transform.Rotate(0, Input.GetAxis("HorizontalRotation") * rotationSpeed * Time.deltaTime, 0);
    }

    public void TakeDamage(int amount)
    {
        health.Change(-amount);
    }
}
