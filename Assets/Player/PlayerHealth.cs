using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sub to OnPlayerDamageTaken, OnPlayerDeath. 
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject damageTakenImpactEffect;

    public UnityEvent<float, float> OnPlayerDamageTaken; // dmgTaken, updatedHealth
    public UnityEvent<float, float> OnPlayerHealed; // healingReceived, updatedHealth
    public UnityEvent<float> OnPlayerDeath;

    public float Health {get; private set;}
    public float MaxHealth { get; private set;}
    public PlayerData playerData;

    void Awake()
    {
        MaxHealth = playerData.health;
        Health = playerData.health;
    }

    public void Damage(float dmgTaken)
    {
        Health = Mathf.Max(0, Health - dmgTaken);
        
        OnPlayerDamageTaken?.Invoke(dmgTaken, Health);

        GameObject damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 3f, 0f), Quaternion.Euler(0f, 0f, 0f));
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
        damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 3f, 0f), Quaternion.Euler(0f, 0f, 90f));
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
        damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 3f, 0f), Quaternion.Euler(0f, 0f, 180f));
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);
        damageImpact = Instantiate(damageTakenImpactEffect, transform.position + new Vector3(0f, 3f, 0f), Quaternion.Euler(0f, 0f, -90f));
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(EnemySpawner.Instance.GenerateRandomColor(), 2.5f);

        if (Health == 0)
        {
            OnPlayerDeath?.Invoke(dmgTaken);
        }
    }

    public void Heal(float amt)
    {
        Health = Mathf.Min(MaxHealth, Health + amt);
        OnPlayerHealed?.Invoke(amt, Health);
    }
}