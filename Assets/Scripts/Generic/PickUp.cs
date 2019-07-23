using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType
    {
        speed,
        colourChange
    }
    public PickupType myType;

    public float speedMultiValue;
    public int cooldownValue = 3;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject player = col.gameObject;
            switch (myType)
            {
                case PickupType.speed:
                    player.GetComponent<PlayerStats>().speedMulti = speedMultiValue;
                    player.GetComponent<PlayerStats>().UpdateSpeed(cooldownValue);
                    break;

                //case PickupType.colourChange:
                    //rend.material.color = Color.red;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
