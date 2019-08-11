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

    public int speedCooldown = 1;
    public float speedMultiValue = 1f;

    public int healthCooldown = 1;
    public int healthBuff = 1;

    public int damageCooldown = 1;
    public float beamDamageMulti = 1f;
    public int baseDamageMulti = 1;

    public static bool spawned = false;       
     
    private void Start()
    {
        spawned = true;        
    }


    private void OnTriggerEnter(Collider col)
    {
        PlayerControl player = col.GetComponent<PlayerControl>();
        if (player != null)
        {
            if(player.GetComponent<PlayerStats>().pickUpActive != true)
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
                        player.GetComponent<BeamAttack>().damageMulti = beamDamageMulti;
                        player.GetComponent<BeamAttack>().UpdateDamage(damageCooldown);

                        player.GetComponent<BaseWeapon>().damageMulti = baseDamageMulti;
                        player.GetComponent<BaseWeapon>().UpdateDamage(damageCooldown);

                        DestroyObject();
                    break;
                }

            }
            
            
        }
        
    }
    
    private void DestroyObject()
    {
        Destroy(gameObject);
        spawned = false;
    }
}
