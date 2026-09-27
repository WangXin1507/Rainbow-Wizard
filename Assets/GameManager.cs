using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float waveMaxSpawnDelayReduction = 0.1f;
    [SerializeField] public List<float> waveBudgetList;
    [SerializeField] public float waveBudgetOverflowIntrestRate = 100f;
    [SerializeField] public float waveBudgetOverflowAccruedInterest = 0f;
    [SerializeField] public List<EnemySpawnData> enemySpawnPool;

    public int currentWave = 0;

    private void Awake()
    {
        Instance = this;
        waveBudgetOverflowAccruedInterest = waveBudgetList[waveBudgetList.Count - 1];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RunWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunWavePrologue()
    {
        RunWave();
    }

    public void RunWave()
    {
        currentWave++;
        Debug.Log("Wave " + currentWave);
        float newWaveBudget = 0f;
        if (currentWave < waveBudgetList.Count)
        {
            newWaveBudget = waveBudgetList[currentWave];
        }
        else
        {
            waveBudgetOverflowAccruedInterest += waveBudgetOverflowIntrestRate;
            newWaveBudget = waveBudgetOverflowAccruedInterest;
        }
        EnemySpawner.Instance.PrepareWaveSequence(newWaveBudget);
        EnemySpawner.Instance.RunWaveSequence();
    }

    public void RunWaveEpilogue()
    {
        RunWavePrologue();
    }
}
