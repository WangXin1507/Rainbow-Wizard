using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sub to OnPlayerDamageTaken, OnPlayerDeath. 
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public UnityEvent<float> OnPlayerDamageTaken;
    public UnityEvent<float> OnPlayerDeath;

    public float Health {get; private set;}

    public void Damage(float amt)
    {
        Health = Mathf.Max(0, Health - amt);
        
        OnPlayerDamageTaken?.Invoke(amt);

        if (Health == 0)
        {
            OnPlayerDeath?.Invoke(amt);
        }
    }
}