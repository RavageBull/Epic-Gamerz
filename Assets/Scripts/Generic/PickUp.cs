using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType
    {
        speed,
        maxHealth,
        damage
    }
    public PickupType myType;

    public float speedMultiValue;
    public int cooldownValue = 3;
    public static bool spawned = false;

    //public Material matofobject;
    public Color newcol;

    private void Start()
    {
        spawned = true;        
    }


    private void OnTriggerEnter(Collider col)
    {
        PlayerControl player = col.GetComponent<PlayerControl>();
        if (player != null)
        {
            
                switch (myType)
                {
                    case PickupType.speed:
                        player.GetComponent<PlayerStats>().speedMulti = speedMultiValue;
                        player.GetComponent<PlayerStats>().UpdateSpeed(cooldownValue);                                          
                        DestroyObject();
                        break;

                    case PickupType.maxHealth:
                        player.GetComponent<Health>().maxhealth += 25;
                        DestroyObject();
                        break;

                    case PickupType.damage:
                        player.GetComponent<BeamAttack>().damage *= 2;
                        player.GetComponent<BeamAttack>().
                        DestroyObject();
                        break;
                }
            
        }
        
    }
    
    private void DestroyObject()
    {
        Destroy(gameObject);
        spawned = false;
    }
}
