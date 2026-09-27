using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private float spawnDistance = 25f;
    [SerializeField] private float spawnMaxYDistance = 12f;
    [SerializeField] private float minSpawnDelay = 1.5f;
    [SerializeField] private float maxSpawnDelay = 4f;
    //[SerializeField] private List<EnemySpawnData> enemySpawnPool;


    [SerializeField] private float waveBudget = 100f;
    [SerializeField] private int enemyCount = 1;
    [SerializeField] private List<EnemySpawnData> enemyWaveSequence;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PrepareWaveSequence(waveBudget);
        //RunWaveSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareWaveSequence(float newWaveBudget)
    {
        if (maxSpawnDelay <= minSpawnDelay)
        {
            minSpawnDelay -= GameManager.Instance.waveMaxSpawnDelayReduction;
        }
        else
        {
            maxSpawnDelay -= GameManager.Instance.waveMaxSpawnDelayReduction;
        }

        waveBudget = newWaveBudget;
        enemyWaveSequence.Clear();

        while (waveBudget > 0f && IncreaseWaveSequence()) ;

        Debug.Log("Wave Ready");
    }

    private bool IncreaseWaveSequence()
    {
        int fallThroughDirection = (UnityEngine.Random.value < 0.5f) ? -1 : 1;
        int enemySpawnIndex = Mathf.FloorToInt(UnityEngine.Random.Range(0f, GameManager.Instance.enemySpawnPool.Count - 0.1f));
        for (int i = 0; i < GameManager.Instance.enemySpawnPool.Count; i++)
        {
            if (TryAddEnemySpawnToWaveSequence(enemySpawnIndex)) return true;
            enemySpawnIndex += fallThroughDirection;
            if (enemySpawnIndex >= GameManager.Instance.enemySpawnPool.Count) enemySpawnIndex = 0;
        }
        return false;
    }

    private bool TryAddEnemySpawnToWaveSequence(int index)
    {
        EnemySpawnData potentialEnemySpawn = GameManager.Instance.enemySpawnPool[index];
        if (potentialEnemySpawn.spawnCost <= waveBudget && potentialEnemySpawn.firstWave <= GameManager.Instance.currentWave)
        {
            enemyWaveSequence.Add(potentialEnemySpawn);
            waveBudget -= potentialEnemySpawn.spawnCost;
            return true;
        }
        return false;
    }

    public void RunWaveSequence()
    {
        StartCoroutine(WaveSequenceCoroutine());
    }

    private IEnumerator WaveSequenceCoroutine()
    {
        while (enemyWaveSequence.Count > 0)
        {
            SpawnEnemySpawn(enemyWaveSequence[0]);
            enemyWaveSequence.RemoveAt(0);
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private void SpawnEnemySpawn(EnemySpawnData enemySpawnData)
    {
        // choose spawn position
        Vector3 waveSpawnPoint = GenerateRandomWaveSpawnPoint();

        for (int i = 0; i < enemySpawnData.groupCount; i++)
        {
            // apply variance
            Vector3 spawnVariation = new Vector3(UnityEngine.Random.Range(-enemySpawnData.groupRadius, enemySpawnData.groupRadius),
                                                 UnityEngine.Random.Range(-enemySpawnData.groupRadius, enemySpawnData.groupRadius),
                                                 0f);
            Vector3 enemySpawnPoint = waveSpawnPoint + spawnVariation;
            SpawnEnemy(enemySpawnData.enemy, enemySpawnPoint);
        }
    }

    private void SpawnEnemy(GameObject enemy, Vector3 position)
    {
        GameObject enemyObject = Instantiate(enemy, position, Quaternion.identity);
        Enemy enemyEnemy = enemyObject.GetComponent<Enemy>();
        if (enemyEnemy.color != PaintColor.NONE) return;
        PaintColor randomColor = GenerateRandomColor();
        enemyEnemy.InitializeEnemy(randomColor);
        enemyCount++;
    }

    public Vector3 GenerateRandomWaveSpawnPoint()
    {
        Vector3 direction = (new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f)).normalized;
        direction *= spawnDistance;
        direction.y = Mathf.Clamp(direction.y, -spawnMaxYDistance, spawnMaxYDistance);
        return direction;
    }

    public PaintColor GenerateRandomColor()
    {
        int randomColor = (GameManager.Instance.currentWave == 1) ? UnityEngine.Random.Range(0, 3) : UnityEngine.Random.Range(0, 6);
        return randomColor switch
        {
            0 => PaintColor.RED,
            1 => PaintColor.YELLOW,
            2 => PaintColor.BLUE,
            3 => PaintColor.ORANGE,
            4 => PaintColor.GREEN,
            5 => PaintColor.PURPLE,
            _ => PaintColor.RED,
        };
    }

    public void DecrementEnemyCount()
    {
        enemyCount--;
        if (enemyCount == 0 && enemyWaveSequence.Count <= 0) GameManager.Instance.RunWaveEpilogue();
    }
}


[Serializable] public struct EnemySpawnData
{
    public GameObject enemy;
    public float spawnCost;
    public int groupCount;
    public float groupRadius;
    public float firstWave;
}
