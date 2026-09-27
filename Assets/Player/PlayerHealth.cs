using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sub to OnPlayerDamageTaken, OnPlayerDeath. 
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public UnityEvent<float, float> OnPlayerDamageTaken; // dmgTaken, updatedHealth
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

        if (Health == 0)
        {
            OnPlayerDeath?.Invoke(dmgTaken);
        }
    }
}