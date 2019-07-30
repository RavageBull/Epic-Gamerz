using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    PlayerControl player;
    Health playerHealth;

    private int healthGiven = 10;

    private float delay = 0f;
    private float delayReset = 2f;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerControl>();
        if (player != null)
        {
            UpdateHealth();
        }
    }

    void UpdateHealth()
    {
        playerHealth = player.GetComponent<Health>();
        if (delay <= 0)
        {
            playerHealth.Change(healthGiven);
            Debug.Log(healthGiven + "  health has been given");
            delay = delayReset;
            Debug.Log("Delay has been Reset");
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }
}
