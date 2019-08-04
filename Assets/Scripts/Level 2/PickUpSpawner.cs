
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject[] pickupPrefab;

    public Transform[] spawnPoints; //Where to spawn 

    public float timeDelay = 10f;
    private float timer;


    private void Start()
    {
        timer = timeDelay;
    }

    private void Update()
    {
        if (!PickUp.spawned)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = timeDelay;
                SpawnRandomPickup();
            }       
        }
    }

    private void SpawnRandomPickup()
    {
        GameObject go = Instantiate(pickupPrefab[Random.Range(0, pickupPrefab.Length)]);
        go.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }

}
