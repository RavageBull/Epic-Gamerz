using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    }

    private SpawnState state = SpawnState.Counting;
          
     [Serializable]
    public class Wave
    {
        public string name; // Name because I can
        public SpawnType[] spawnType;
        public float delay; // Time between each spawn
    }

    public Wave[] waves; // List of waves to iterate through
    private int waveIndex = 0; // Wave Index

    [Serializable]
    public class SpawnType
    {
        public string name;
        public GameObject enemy;
        public int count;
    }

    public List<GameObject> thisWave = new List<GameObject>(); //Keeping track of what is currently spawning

    public Transform[] spawnPoints; // Where to spawn

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public Text RoundUI;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        RoundUI.text = waveIndex + 1.ToString("0");

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced.");
        }
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }

        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                //Start spawning wave
                InitializeWave(waves[waveIndex]);
                RoundUI.text = (waveIndex + 1).ToString("0");
                ShuffleWave();
                StartCoroutine(SpawnWave(waves[waveIndex].delay));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }

        return true;
    }

    private void WaveCompleted()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (waveIndex > waves.Length)
        {
            waveIndex = 0;
            //currently resets wave loop, but this is where we would trigger end level
        }
        else
        {
            waveIndex++;
        }
    }

    private void InitializeWave(Wave _wave)
    {
        thisWave = new List<GameObject>();

        foreach (SpawnType i in _wave.spawnType)
        {
            for (int j = 0; j < i.count; j++)
            {
                thisWave.Add(i.enemy);
            }
        }
    }

    private void ShuffleWave() // Shuffles the list
    {
        for(int i = 0; i < 25; i++)
        {
            int randOne = Random.Range(0, thisWave.Count);
            int randTwo = Random.Range(0, thisWave.Count);
            
            GameObject temp = thisWave[randOne];
            thisWave[randOne] = thisWave[randTwo];
            thisWave[randTwo] = temp;
        }

        //Debug.Log("List Shuffled");
    }
               
    private IEnumerator SpawnWave(float delay)
    {        
        state = SpawnState.Spawning;
        
        for (int i = 0; i < thisWave.Count; i++)
        {
            SpawnEnemy(thisWave[i]);
            yield return new WaitForSeconds(delay);
        }

        state = SpawnState.Waiting;        
    }

    private void SpawnEnemy(GameObject _enemy)
    {
        //Spawn Enemy
        //Debug.Log("Spawning Enemy: " + _enemy.name);
               
        Transform randSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //picks a random spawn point
        Instantiate(_enemy, randSpawnPoint.position, randSpawnPoint.rotation);
    }
}
