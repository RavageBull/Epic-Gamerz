using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxhealth = 100; //Maximum health of player
    public int maxHealthReset = 100;

    public int maxHealthBuff;
    public int myHealth;

    public int CurrentHealth { get; private set; }

    
    public event Action OnDeath; // On death event

    public Slider healthbar;
    public GameObject healthUI;

    public Text maxHealthCounter;
        
    private GameManager gameManager;

    private void Start()
    {
        CurrentHealth = maxhealth;
        healthbar.maxValue = maxhealth;
        healthbar.value = CurrentHealth;

        maxHealthCounter.text = maxhealth.ToString("0");

        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        myHealth = CurrentHealth;
        healthbar.maxValue = maxhealth;
        healthbar.value = CurrentHealth;
    }

    // Change the health amount
    public void Change(int amount)
    {
        
         CurrentHealth += amount;
       
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


        healthbar.value = CurrentHealth;
    }

    public void UpdateMaxHealth(int cooldown)
    {
        maxhealth = maxhealth * maxHealthBuff;
        CurrentHealth = maxhealth;
        StartCoroutine(HealthCooldown(cooldown));

        GetComponent<PlayerStats>().pickUpActive = true;
        healthUI.SetActive(true);
        maxHealthCounter.color = Color.green;
        maxHealthCounter.text = maxhealth.ToString("0");
    }

    IEnumerator HealthCooldown(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetHealth();
    }

    public void ResetHealth()
    {
        maxhealth = maxHealthReset;
        maxHealthBuff = 0;

        GetComponent<PlayerStats>().pickUpActive = false;

        healthUI.SetActive(false);
        maxHealthCounter.color = Color.white;
        maxHealthCounter.text = maxhealth.ToString("0");
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

        Destroy(gameObject);
        
    }

}
