using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //Public Variables
    public GameObject prefabToSpawn;
    public float spawnTime;
    public float spawnTimeRandom;
    //Private Variables
    private float spawnTimer;

    //Used for initialisation
    void Start()
    {
        ResetSpawnTimer();
    }

    //Update is called once per frame
    void Update()
    {
        if(spawnTime > 0.5f)
        {
            spawnTime = spawnTime - (Time.deltaTime / 33);
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            ResetSpawnTimer();
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()
    {
        spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }
}
