using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public MessageFrame messageFrame;

    [SerializeField] public float waveMaxSpawnDelayReduction = 0.1f;
    [SerializeField] public List<float> waveBudgetList;
    [SerializeField] public float waveBudgetOverflowIntrestRate = 100f;
    [SerializeField] public float waveBudgetOverflowAccruedInterest = 0f;
    [SerializeField] public float postWaveHealing = 1000f;
    [SerializeField] public float points = 0f;
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
        RunWavePrologue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunWavePrologue()
    {
        //Player.PlayerHealth.Heal(postWaveHealing);
        //RunWave();
        StartCoroutine(PrologueCoroutine());
    }

    private IEnumerator PrologueCoroutine()
    {
        if (currentWave <= 0) yield return new WaitForSeconds(1f);
        messageFrame.ShowMessage("Wave " + (currentWave + 1));
        GameAudioManager.Instance.PlayPointEarned();
        if (currentWave > 0) yield return new WaitForSeconds(1f);
        Player.PlayerHealth.Heal(postWaveHealing);
        RunWave();
        yield return new WaitForSeconds(1.5f);
        messageFrame.HideMessage();
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
        StartCoroutine(EpilogueCoroutine());
    }

    private IEnumerator EpilogueCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        messageFrame.ShowMessage("Wave " + currentWave + "\ncomplete!");
        GameAudioManager.Instance.PlayVictoryHorn();
        yield return new WaitForSeconds(2.5f);
        messageFrame.ShowMessage(SelectRandomMessage());
        GameAudioManager.Instance.PlayPointEarned();
        yield return new WaitForSeconds(5f);
        RunWavePrologue();
    }

    private string SelectRandomMessage()
    {
        int randomMessage = UnityEngine.Random.Range(0, 13);
        return randomMessage switch
        {
            0 => "Looking good so far!",
            1 => "I love the contrast…",
            2 => "There are no mistakes, only happy little accidents.",
            3 => "A true work of art in progress!",
            4 => "Worth a thousand words!",
            5 => "Like music to the eyes!",
            6 => "Bravo!",
            7 => "Like watching a true master at work…",
            8 => "We’re not done just yet!",
            9 => "A modern day Pollock, if I do say so myself.",
            10 => "Really getting an arm workout in, huh?",
            11 => "Really getting an arm workout in, huh?",
            12 => "Abra-color-dabra, abra-color-zam!!",
            _ => "Looking good so far!",
        };
    }
}
