using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject enemies;
    [SerializeField] float timeBetweenwaves = 3f;
    private float countdown = 1f;
    private int waveNumber = 0;
    public EnemySpawn[] enemySpawns;
    private int LevelBuff;
    public MainManager mainManager;

    void Start()
    {
        EnemiesAlive = 0;
        LevelBuff = (LevelSelector.currentLevel-1)%3;
        Debug.Log("Level:"+LevelBuff);
    }
    
    void Update()
    {
        if(EnemiesAlive > 0)
        {
            return ;
        }

        if(countdown <= 0f)
        {
            if(waveNumber == 5+LevelBuff*5)
            {
                mainManager.WinLevel(LevelSelector.currentLevel);
                this.enabled = false;
                return ;
            }

            waveNumber++;        
            PlayerStats.Rounds++;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenwaves;
            return ;
        }
        // Debug.Log(countdown);
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Debug.Log(waveNumber);
        
        foreach(EnemySpawn es in enemySpawns)
        {
            if(waveNumber < es.startLevel)
            {
                continue;
            }
            EnemiesAlive += es.baseCount + (waveNumber-es.startLevel)/5 * es.incCount + LevelBuff;
        }
        Debug.Log(EnemiesAlive);

        foreach(EnemySpawn es in enemySpawns)
        {
            if(waveNumber < es.startLevel)
            {
                continue;
            }

            int cnt = es.baseCount + (waveNumber-es.startLevel)/5 * es.incCount + LevelBuff;
            float rate = 1f / (es.baseRate + (waveNumber-es.startLevel)/5 * es.incRate + LevelBuff);
            Debug.Log("Spawn:"+cnt);

            for(int i=0; i<cnt; i++)
            {
                SpawnEnemy(es.enemy);
                yield return new WaitForSeconds(rate);
            }
        }

        
        
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Transform e = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation).transform;
        e.SetParent(enemies.transform);
        Debug.Log("spawn!");
    }
}
