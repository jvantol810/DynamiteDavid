using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectOnCycle : MonoBehaviour
{
    public float timeBetweenSpawn;
    public GameObject objectToSpawn;
    protected float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetweenSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Spawn();
        }
        timer -= Time.deltaTime;
    }

    public virtual GameObject Spawn()
    {
        GameObject spawnedObj = Instantiate(objectToSpawn);
        timer = timeBetweenSpawn;
        return spawnedObj;
    }

}
