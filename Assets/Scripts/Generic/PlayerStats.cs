using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerControl pController;
    Health health;

    public float speed;
    public float defaultSpeed;
    public float speedMulti;

    [System.NonSerialized]public bool keyObtained = false;

    public void Start()
    {
        pController = GetComponent<PlayerControl>();
        health = GetComponent<Health>();
        defaultSpeed = pController.agent.speed; // sets the speed to the current player speed
        speed = defaultSpeed;
    }

    public void Update()
    {
        pController.agent.speed = speed;
        
    }

    public void UpdateSpeed(int cooldown)
    {
        speed = defaultSpeed * speedMulti;
        pController.agent.speed = speed; // sets the current player speed to the speed variable on this script
        StartCoroutine(SpeedCooldown(cooldown));
    }

    IEnumerator SpeedCooldown(int waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        speed = defaultSpeed;
    }

    public void TakeDamage(int amount)
    {
        health.Change(-amount);
    }
}
//hello
