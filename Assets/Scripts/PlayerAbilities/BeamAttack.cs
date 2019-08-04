using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamAttack : MonoBehaviour
{
    public GameObject currentHitObject;

    public float damage = 1f;  //sets the damage of the rays being cast
    public float maxDistance = 50f;  //sets the range of the ability
    public float sphereRadius;
    public LayerMask layerMask;
    public float castTime = 0.1f;

    private float fireRate = 8f;  //the amount of time in seconds before the ability can be used again
    private float cooldown = 0f;  //used to create a timer with Time.time and firerate so the ability cannot be used all the time
    private float abilityTime = 3f;  //the amount of time that the beam attack casts for

    public Vector3 firePoint;
    private Vector3 direction;

    private float currentHitDistance;

    PlayerControl PController;
    //PlayerMovement otherController; //just a reference to the enemy testing player script

   // public Slider abilitySlider;
    public Text timer;
    public Slider clock;

    void Start()
    {
        PController = GetComponentInParent<PlayerControl>();
        //otherController = GetComponentInParent<PlayerMovement>();
        cooldown = fireRate;

        timer.text = cooldown.ToString("0");
        clock.maxValue = fireRate;
        clock.value = cooldown;

    }

    // Update is called once per frame
    void Update()
    {        
        if (PController != null)
        {
            if (cooldown <= 0f)  //if player uses fire2 and the timer is above or equal to the cooldown then it proceeds
            {
                timer.color = Color.red;
                timer.text = ("B");

                if (Input.GetButtonDown(PController.F2))
                {
                    timer.color = Color.white;
                    cooldown = fireRate;  //the cooldown equals the timer which counts up, and if it has been the amount of the firerates seconds then itll Cast
                    Debug.Log("Cooldown time is " + cooldown);

                    //Debug.Log("Fired");
                    Cast();  //Cast referce to void Cast() where it shoots the raycast for the ability

                }
            }
            else
            {                
                timer.text = cooldown.ToString("0");
                clock.value = cooldown;
                cooldown -= Time.deltaTime;
            }
        }

        

    }


    void Cast()
    {
        firePoint = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(firePoint, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;

            Invoke("Cast", castTime);  //starts Cast again based on the public float castTime
            EnemyStats target = hit.transform.GetComponent<EnemyStats>();
            if (target != null)
            {
                Debug.Log(hit.transform.name);
                target.TakeDamage(damage);
            }
        }
        
        StartCoroutine("WaitAndExecute");
        Invoke("StopExecution", abilityTime); //when abilityTime is reached it calls StopExecution
    }


    void StopExecution()
    {
        //Debug.Log("Ability pause done");
        StopCoroutine("WaitAndExecute");  //stops StartCoroutine
        CancelInvoke("Cast");  //Stops Cast from repeating once abilityTime is met
    }

    IEnumerator WaitAndExecute()
    {
        //print("Printed after wait time");
        yield return new WaitForSeconds(abilityTime);

        // StartCoroutine("WaitAndExecute");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(firePoint, firePoint + direction * currentHitDistance);
        Gizmos.DrawWireSphere(firePoint + direction * currentHitDistance, sphereRadius);
    }


}






