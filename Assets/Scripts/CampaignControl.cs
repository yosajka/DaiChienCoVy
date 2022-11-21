using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class CampaignControl : MonoBehaviour
{
    private BezierFollow bf;

    [SerializeField]
    private PathCreator[] pathCreator;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Transform[] spawnPos;

    public float speed = 5f;
    private int numOfEnemiesPhase1;
    private int numOfEnemiesPhase2;
    private int numOfEnemiesPhase3;
    private int numOfKilledEnemies;
    public GameObject orbSpawners;
    enum Phase
    {
        Phase_1,
        Phase_2,
        Phase_3,
        Victory
    }
    private Phase phase;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateSpawnOrb()
    {
        switch (phase)
        {
            case Phase.Phase_1:
                SpawnOrbInPhase1();
                break;
            case Phase.Phase_2:
                SpawnOrbInPhase2();
                break;
            case Phase.Phase_3:
                SpawnOrbInPhase3();
                break;
        }
    }

    private void SpawnOrbInPhase3()
    {
        return;
    }

    private void SpawnOrbInPhase2()
    {
        return;
    }

    private void SpawnOrbInPhase1()
    {
        OrbSpawner[] orbspawns = orbSpawners.GetComponents<OrbSpawner>();
        Debug.Log(numOfKilledEnemies);
        if (numOfKilledEnemies == numOfEnemiesPhase1 / 4)
        {
            orbspawns[(int)OrbControl.OrbType.BlueBullet].SpawnOrbOneTime();
            orbspawns[(int)OrbControl.OrbType.RedBullet].SpawnOrbOneTime();
        }
        else if (numOfKilledEnemies == numOfEnemiesPhase1 / 2)
        {
            orbspawns[(int)OrbControl.OrbType.Shield].SpawnOrbOneTime();
        }
        else if (numOfKilledEnemies == numOfEnemiesPhase1 / 8)
        {
            orbspawns[(int)OrbControl.OrbType.Speed].SpawnOrbOneTime();
        }
        else if (numOfKilledEnemies == numOfEnemiesPhase1)
        {
            orbspawns[(int)OrbControl.OrbType.Heal].SpawnOrbOneTime();
        }
    }

    private void UpdatePhase()
    {
        switch (phase)
        {
            case Phase.Phase_1:
                // if (IsPhaseCompleted()) phase = Phase.Phase_2;
                if (IsPhaseCompleted()) phase = Phase.Victory;
                break;
            case Phase.Phase_2:
                if (IsPhaseCompleted()) phase = Phase.Phase_3;
                break;
            case Phase.Phase_3:
                if (IsPhaseCompleted()) phase = Phase.Victory;
                break;
        }
        if (phase == Phase.Victory){
            gameObject
            .GetComponent<GameCamp1Manager>()
            .ChangeToVictory();
        }
    }

    public void StartCampaign1()
    {
        Debug.Log("here");
        phase = Phase.Phase_1;
        SpawnPhase1();
    }

    private bool IsPhaseCompleted()
    {
        return phase switch
        {
            Phase.Phase_1 =>
                numOfKilledEnemies == numOfEnemiesPhase1,
            Phase.Phase_2 =>
                numOfKilledEnemies == numOfEnemiesPhase2,
            Phase.Phase_3 =>
                numOfKilledEnemies == numOfEnemiesPhase3,
            _ => false,
        };
    }

    private IEnumerator SpawnYellowEnemies()
    {
        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[2]);
            enemy.transform.position = spawnPos[3].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[3];
            yield return new WaitForSeconds(1.1f);
        }
    }

    public void NoticeDestroyedEnemy()
    {
        numOfKilledEnemies += 1;
        UpdateSpawnOrb();
        UpdatePhase();
    }

    private IEnumerator SpawnBlueEnemies()
    {

        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0]);
            enemy.transform.position = spawnPos[0].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[0];
            yield return new WaitForSeconds(1.1f);
        }
    }

    private IEnumerator SpawnGreenEnemies()
    {
        int numOfEnemies = 12;
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[1]);
            enemy.transform.position = spawnPos[5].position;
            enemy.GetComponent<Follower>().pathCreator = pathCreator[5];
            yield return new WaitForSeconds(1.1f);
        }
    }

    public void SpawnPhase1()
    {
        int numOfEnemies = 9;
        numOfEnemiesPhase1 = 9 * 2;
        float[] posInLine0 = new float[numOfEnemies];
        float[] posInLine5 = new float[numOfEnemies];
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < numOfEnemies; i++)
        {
            posInLine0[i] = (float)(i - numOfEnemies / 2) * max.x / 5;
            posInLine5[i] = (float)(i - numOfEnemies / 2) * max.x / 5;
        }
        StartCoroutine(SpawnEnemyLine(numOfEnemies, posInLine0, 0));
        StartCoroutine(SpawnEnemyLine(numOfEnemies, posInLine5, 5));
    }

    private IEnumerator SpawnEnemyLine(int numOfEnemies, float[] posInLine, int line)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0]);
            enemy.transform.position = spawnPos[line].position;
            Follower follower = enemy.GetComponent<Follower>();
            follower.pathCreator = pathCreator[line];
            follower.SetDestination(posInLine[numOfEnemies - i - 1]);
            follower.SetSpeed(8f);
            yield return new WaitForSeconds(0.5f);
        }
    }



}
