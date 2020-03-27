using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveup : MonoBehaviour
{
    //Used for initialisation
    float spawnTimer = 2;
    float spawnTimeRandom = 4;
    float spawnTime;
    void Start()
    {
        ResetSpawnTimer();
    }

    //Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            transform.position += new Vector3(0, 1, 0);
            ResetSpawnTimer();
        }
    }

    //Resets the spawn timer with a random offset
    void ResetSpawnTimer()
    {
        spawnTimer = (float)(spawnTime + Random.Range(0, spawnTimeRandom * 100) / 100.0);
    }
}
