using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType
    {
        speed,
        colorChange
    }
    public PickupType myType;

    public float speedMultiValue;
    public int cooldownValue = 3;

    //public Material matofobject;
    public Color newcol;
       
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

                    case PickupType.colorChange:
                        player.GetComponent<MeshRenderer>().material.color = newcol;
                        DestroyObject();
                        break;
                }
            
        }
        
    }
    
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
