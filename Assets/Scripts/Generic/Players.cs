using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    /*
      public GameObject followProjectile;
    public GameObject Projectile;
    public Transform SpawnLoc;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //get our child shooting location
            //SpawnLoc = transform.Find("ShootLoc").gameObject.transform;
            //spawn bullet prefab
            Instantiate(Projectile, SpawnLoc.position, SpawnLoc.rotation);

            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 100f)) ;
        }
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Instantiate(followProjectile, SpawnLoc.position, SpawnLoc.rotation);
            }
        }
    }*/
}
