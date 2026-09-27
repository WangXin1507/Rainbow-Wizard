using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnDelay = 0.5f;
    [SerializeField] private float maxSpawnDelay = 3f;
    [SerializeField] private List<EnemySpawnData> enemySpawnPool;


    [SerializeField] private float waveBudget = 100f;
    [SerializeField] private List<EnemySpawnData> enemyWaveSequence;
    [SerializeField] private float enemyCount = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareWaveSequence(waveBudget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepareWaveSequence(float newWaveBudget)
    {
        waveBudget = newWaveBudget;
        enemyWaveSequence.Clear();

        while (waveBudget > 0f && IncreaseWaveSequence()) ;

        Debug.Log("Wave Ready");
    }

    private bool IncreaseWaveSequence()
    {
        int enemySpawnIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0f, enemySpawnPool.Count - 0.1f));
        for (int i = 0; i < enemySpawnPool.Count; i++)
        {
            if (TryAddEnemySpawnToWaveSequence(enemySpawnIndex)) return true;
            enemySpawnIndex++;
            if (enemySpawnIndex >= enemySpawnPool.Count) enemySpawnIndex = 0;
        }
        return false;
    }

    private bool TryAddEnemySpawnToWaveSequence(int index)
    {
        EnemySpawnData potentialEnemySpawn = enemySpawnPool[index];
        if (potentialEnemySpawn.spawnCost <= waveBudget)
        {
            enemyWaveSequence.Add(potentialEnemySpawn);
            waveBudget -= potentialEnemySpawn.spawnCost;
            return true;
        }
        return false;
    }

    private IEnumerator RunWaveSequence()
    {
        while (enemyWaveSequence.Count > 0)
        {
            SpawnEnemySpawn(enemyWaveSequence[0]);
            enemyWaveSequence.RemoveAt(0);
            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnEnemySpawn(EnemySpawnData enemySpawnData)
    {

    }
}


[Serializable] public struct EnemySpawnData
{
    public GameObject enemy;
    public float spawnCost;
    public int groupCount;
    public float groupRadius;
}
