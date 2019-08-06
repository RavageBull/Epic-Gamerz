using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public enum PickupType
    {
        speed,
        maxHealth,
        damage
    }
    public PickupType myType;

    public int speedCooldown = 3;
    public int healthCooldown = 3;
    public int damageCooldown = 3;

    public static bool spawned = false;

    public float speedMultiValue = 5f;
    public int healthBuff = 25;
    public float damageMultiValue = 5f;
    
    //public Color newcol;
     
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
                        player.GetComponent<PlayerStats>().UpdateSpeed(speedCooldown);
                        DestroyObject();
                        break;

                    case PickupType.maxHealth:
                        player.GetComponent<Health>().maxHealthBuff += healthBuff;
                        player.GetComponent<Health>().UpdateMaxHealth(healthCooldown);
                        DestroyObject();
                        break;

                    case PickupType.damage:
                        player.GetComponent<BeamAttack>().damageMulti = damageMultiValue;
                        player.GetComponent<BeamAttack>().UpdateDamage(damageCooldown);
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
