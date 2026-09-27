using System.Collections;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    private GameObject damageTakenImpactEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateDeathSequence(GameObject effect)
    {
        //blah blah blah
        GameManager.Instance.isDead = true;
        GameAudioManager.Instance.PlayVictoryStupid();
        damageTakenImpactEffect = effect;
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 40; i++)
        {
            GameObject damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, 0f));
            damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
            damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, 90f));
            damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
            damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, 180f));
            damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
            damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, -90f));
            damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
            GameAudioManager.Instance.PlayPaintSwitch();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
