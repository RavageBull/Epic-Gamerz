using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.keyObtained = true;
            DestroyObject();
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
