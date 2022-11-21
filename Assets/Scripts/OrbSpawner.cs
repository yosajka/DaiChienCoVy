using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orb;
    [SerializeField]
    private float maxOrbSpawnerRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnOrb()
    {
        SpawnOrbOneTime();
        ScheduleNextOrbSpawn();
    }

    public void SpawnOrbOneTime()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anOrb = (GameObject)Instantiate(orb);
        anOrb.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }

    void ScheduleNextOrbSpawn()
    {
        float spawnInSeconds;

        if (maxOrbSpawnerRate > 1f)
        {
            //pick a number between 1 and maxSpawnRate
            spawnInSeconds = Random.Range(1f, maxOrbSpawnerRate);
        }
        else
        {
            spawnInSeconds = 1f;
        }
        Invoke("SpawnOrb", spawnInSeconds);
    }
    void IncreaseSpawnRate()
    {
        if (maxOrbSpawnerRate > 1f)
        {
            maxOrbSpawnerRate--;
        }
        else
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }


    public void ScheduleOrbSpawner()
    {
        Invoke("SpawnOrb", maxOrbSpawnerRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Function to stop enemy spawner
    public void UnscheduleOrbSpawner()
    {
        CancelInvoke("SpawnOrb");
        CancelInvoke("IncreaseSpawnRate");
    }
}
