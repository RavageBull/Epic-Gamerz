using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth = 100; //Maximum health of player
    public int myHealth;

    public int CurrentHealth { get; private set; }

    
    public event Action OnDeath; // On death event
    public event Action<int> OnHealthChanged; // On health changed event
    
    private void Start()
    {
        CurrentHealth = maxhealth;
    }

    private void Update()
    {
        myHealth = CurrentHealth;
    }

    // Change the health amount
    public void Change(int amount)
    {
        
         CurrentHealth += amount;
                
        //Debug.Log("health is " + CurrentHealth);

        if(OnHealthChanged != null)
        {
            OnHealthChanged(CurrentHealth); //triggers on health changed
        }

        ValidateHealth();       
    }

    // Keep health within values
    private void ValidateHealth()
    {       
        //Player dies
        if (CurrentHealth <= 0)
        {
            Death(); 
            CurrentHealth = 0;
        }

        //if current health goes over max health, reset it to the max
        if (CurrentHealth >= maxhealth)
        {
            CurrentHealth = maxhealth;
        }
    }

    // Do death stuff
    private void Death()
    {
        // Do more death stuff
        if(OnDeath != null)
        {
            OnDeath();
        }

        Debug.Log("Player died " + CurrentHealth);
        GetComponent<MeshRenderer>().material.color = Color.red;

        transform.position = transform.position; //Needs fixing for task 1

        /* Things to do with this script (Script Tasks) */

        // 1. Freeze player in position and stop controller input
        // 2. If both players are dead we want to end the game / send them back to main menu
        // 3. If scope allows it, we want to create a reviving system
    }

}
