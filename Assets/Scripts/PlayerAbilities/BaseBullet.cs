using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{/*
    public float speed = 15;
    public float enemyDamage = 25f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Moves Fowards based on speed
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {

        //if not player or projectile
        if (other.tag != "Player" && other.tag != "Projectile")
        {
            if (other.tag == "Enemy")

            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out hit, 100f)) ;

                hit.transform.GetComponent<HealthScript>().RemoveHealth(enemyDamage);
                //reduce enemy health

                Destroy(this.gameObject);
                //destroy this bullet
            }
        }
    }
*/}

