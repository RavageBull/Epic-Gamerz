using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    PlayerControl playerController;
   
    public int playerNumber = 1;
    public float rotationSpeed = 100f;
    public string F1 = "Fire1P1";
    public string F2 = "Fire2P1";

    //Camera cam;
 
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerControl>();
        //cam = Camera.main;
       
    }

    private void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        float mouseX = Input.GetAxis("RHorizontalP1");
        float mouseY = Input.GetAxis("RVerticalP1");

        //float heading = Mathf.Atan2(mouseX, mouseY);
        //transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);




        /*float cameraDiff = cam.transform.position.y - transform.position.y;
        Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDiff));
        Vector3 turretLookDirection = new Vector3(worldpos.x, transform.position.y, worldpos.z);
        transform.LookAt(turretLookDirection);*/
    }

    void FixedUpdate()
    {
       
        Vector3 moveDirection = new Vector3(-Input.GetAxis("LVerticalP1"), 0, Input.GetAxis("LHorizontalP1"));

        transform.position += moveDirection * playerController.agent.speed * Time.fixedDeltaTime;

        //transform.Rotate(0, Input.GetAxis("HorizontalRotation") * rotationSpeed * Time.deltaTime, 0);
    }

    
}
