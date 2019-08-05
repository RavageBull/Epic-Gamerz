using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
     
    private void OnTriggerStay(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if(player != null)
        {
            if (player.keyObtained == true)
            {
                player.keyObtained = false;              
                DestroyObject();

            }
        }
    }

    private void DestroyObject()
    {
                Destroy(gameObject);
    }
}
