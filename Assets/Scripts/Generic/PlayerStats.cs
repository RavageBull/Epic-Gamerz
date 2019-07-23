using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerControl pController;

    public float speed;
    private float defaultSpeed = 10f;
    public float speedMulti;

    public void Start()
    {
        pController = GetComponent<PlayerControl>();
        speed = pController.agent.speed; // sets the speed to the current player speed
        //speed = defaultSpeed;
    }

    public void Update()
    {
        pController.agent.speed = speed;
        
    }

    public void UpdateSpeed(int cooldown)
    {
        speed = defaultSpeed * speedMulti;
        pController.agent.speed = speed; // sets the current player speed to the speed of this script
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
}
//hello
