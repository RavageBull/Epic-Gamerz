using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private PlayerControl pController;
    public Health health;

    public float speed;
    public float defaultSpeed;
    public float speedMulti;

    public GameObject speedUI;
    public GameObject keyUI;

    [System.NonSerialized] public bool keyObtained = false;
    [System.NonSerialized] public bool pickUpActive = false;

    private Gate[] gates;

    private void OnEnable()
    {
        health.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        health.OnDeath -= OnDeath;
    }

    public void Start()
    {
        pController = GetComponent<PlayerControl>();
        defaultSpeed = pController.agent.speed; // sets the speed to the current player speed
        speed = defaultSpeed;
    }

    public void Update()
    {
        pController.agent.speed = speed;
        
    }

    public void UpdateSpeed(int cooldown)
    {
        speedUI.SetActive(true);
        pickUpActive = true;

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
        speedMulti = 0;
        pickUpActive = false;

        speedUI.SetActive(false);
        pickUpActive = false;
    }

    public void TakeDamage(int amount)
    {
        health.Change(-amount);
    }

    private void OnDeath()
    {
       if (keyObtained == true)
        {
            gates = FindObjectsOfType<Gate>();

            if (gates.Length > 0)
            {
                foreach (Gate gate in gates)
                {
                    Destroy(gate.gameObject);
                }
            }
            
        }
    }
}
