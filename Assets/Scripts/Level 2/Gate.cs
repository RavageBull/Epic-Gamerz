using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Vector3 yPos;
    public int gateNum = 1;
    public GameObject key;
    public GameObject spawnedKey;
 
    private void OnTriggerStay(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if(player != null)
        {
            if (player.keyObtained == true)
            {
                Key key = GetComponent<Key>();
                if (gateNum == key.keyNum)
                {
                    Debug.Log("I can be opened");
                }
               
//                DestroyObject();

            }
        }
    }

    private void DestroyObject()
    {
                Destroy(gameObject);
    }
}
