using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject asteroid;

    float maxEnemySpawnRate = 5f; //seconds
    float maxAsteroidSpawnRate = 8f; //seconds
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemy() {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //spawn enemy
        GameObject anEnemy = (GameObject)Instantiate(enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn("enemy");
    }

    void spawnAsteroid() {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        //spawn enemy
        GameObject anAsteroid = (GameObject)Instantiate(asteroid);
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn("asteroid");
    }

    void ScheduleNextEnemySpawn(string enemyName) {
        float spawnInSenconds;
        if (enemyName == "enemy") {
            
            if (maxEnemySpawnRate > 1f) {
                //pick a number between 1 and maxSpawnRate
                spawnInSenconds = Random.Range(1f, maxEnemySpawnRate);
            }
            else {
                spawnInSenconds = 1f;
            }
            Invoke("spawnEnemy", spawnInSenconds);
        }
        else if (enemyName == "asteroid") {
            if (maxAsteroidSpawnRate > 1f) {
                //pick a number between 1 and maxSpawnRate
                spawnInSenconds = Random.Range(4f, maxAsteroidSpawnRate);
            }
            else {
                spawnInSenconds = 1f;
            }
            Invoke("spawnAsteroid", spawnInSenconds);
        }    
    }

    //Function to increase difficulty
    void IncreaseSpawnRate() {
        if (maxEnemySpawnRate > 1f) {
            maxEnemySpawnRate--;
        }
        else {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    // Function to start Enemy spawner
    public void ScheduleEnemySpawner() {
        float maxEnemySpawnRate = 5f; //seconds
        float maxAsteroidSpawnRate = 8f; //seconds
        
        Invoke("spawnEnemy", maxEnemySpawnRate);
        Invoke("spawnAsteroid", maxAsteroidSpawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Function to stop enemy spawner
    public void UnscheduleEnemySpawner() {
        CancelInvoke("spawnEnemy");
        CancelInvoke("spawnAsteroid");
        CancelInvoke("IncreaseSpawnRate");
    }
}
