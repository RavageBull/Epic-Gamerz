using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{

    public float damage = 1f;  //sets the damage of the rays being cast
    public float range = 50f;  //sets the range of the ability
    public Transform firePoint;  //empty game object on the player where the raycast comes from

    private float fireRate = 8f;  //the amount of time in seconds before the ability can be used again
    private float cooldown = 0f;  //used to create a timer with Time.time and firerate so the ability cannot be used all the time
    private float abilityTime = 3f;  //the amount of time that the beam attack casts for
    [SerializeField]private float castTime = 0.2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && Time.time >= cooldown)  //if player uses fire2 and the timer is above or equal to the cooldown then it proceeds

        {

            cooldown = Time.time + fireRate;  //the cooldown equals the timer which counts up, and if it has been the amount of the firerates seconds then itll Cast
            Debug.Log("Cooldown time is " + cooldown);

            Debug.Log("Fired");
            Cast();  //Cast referce to void Cast() where it shoots the raycast for the ability

        }


    }


    void Cast()
    {

        RaycastHit hit;  //sets up the local variable hit and can tell us what the Raycast has hit

        Debug.DrawLine(firePoint.position, (firePoint.forward * range), Color.red, abilityTime);  //draws a red line forwards from the firepoint position on the player

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range)) //an if statement that tell us if the Raycast has hit an object
        {
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))

                Debug.Log("Raycast hit this thing " + hit.transform.name);

            Invoke("Cast", castTime);  //starts Cast again every 0.2 seconds
            EnemyStats target = hit.transform.GetComponent<EnemyStats>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        //Invoke("Cast", castTime);  //starts Cast again every 0.2 seconds
        StartCoroutine("WaitAndExecute");
        Invoke("StopExecution", abilityTime); //when abilityTime is reached it calls StopExecution
    }


    void StopExecution()
    {
        Debug.Log("Ability pause done");
        StopCoroutine("WaitAndExecute");  //stops StartCoroutine
        CancelInvoke("Cast");  //Stops Cast from repeating once abilityTime is met
    }

    IEnumerator WaitAndExecute()
    {
        print("Printed after wait time");
        yield return new WaitForSeconds(abilityTime);

        // StartCoroutine("WaitAndExecute");
    }


}






