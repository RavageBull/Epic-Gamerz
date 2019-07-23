using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    private float defaultSpeed = 6.0f;
    public float speedMulti; 

    public void UpdateSpeed(int cooldown)
    {
        speed = defaultSpeed * speedMulti;
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

