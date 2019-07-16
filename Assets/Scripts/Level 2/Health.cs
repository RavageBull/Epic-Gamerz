using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxhealth = 100;

    public int CurrentHealth { get; private set; }
    public int currentHealth;

    public event Action OnDeath; // Subscribe to me
    public event Action<int> OnHealthChanged;
    
    private void Start()
    {
        CurrentHealth = maxhealth;
        currentHealth = CurrentHealth;
    }

    // Change the health amount
    public void Change(int amount)
    {
        CurrentHealth += amount;
        Debug.Log("health is " + CurrentHealth);

        if(OnHealthChanged != null)
        {
            OnHealthChanged(CurrentHealth);
        }
        ValidateHealth();       
    }

    // Keep health within values
    private void ValidateHealth()
    {
        currentHealth = CurrentHealth;
        if (CurrentHealth < 0)
        {
            Death();
            CurrentHealth = 0;
        }

        if (CurrentHealth > maxhealth)
        {
            CurrentHealth = maxhealth;
        }
    }

    // Do death stuff
    private void Death()
    {
        // Do death stuff
        if(OnDeath != null)
        {
            OnDeath();
        }
        Debug.Log("Player died " + CurrentHealth);
        GetComponent<MeshRenderer>().material.color = Color.red;
        transform.position = transform.position;
        // Destroy(gameObject);
    }

}
