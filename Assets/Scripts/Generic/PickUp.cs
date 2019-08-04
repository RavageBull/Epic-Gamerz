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
    public static bool spawned = false;

    //public Material matofobject;
    public Color newcol;

    private void Start()
    {
        spawned = true;
        SetColour();
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

                    case PickupType.colorChange:
                        player.GetComponent<MeshRenderer>().material.color = newcol;
                        DestroyObject();
                        break;
                }
            
        }
        
    }

    void SetColour()
    {
        switch (myType)
        {
            case PickupType.speed:
                GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                break;

            case PickupType.colorChange:
                GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
                break;

        }
    }


    private void DestroyObject()
    {
        Destroy(gameObject);
        spawned = false;
    }
}
