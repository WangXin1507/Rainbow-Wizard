using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sub to OnEnemyDamageTaken, OnEnemyDeath. 
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public UnityEvent<float> OnEnemyDamageTaken;
    public UnityEvent<float> OnEnemyDeath;

    [SerializeField] private float baseMaxHealth;

    public float redHealth { get; private set; }
    public float yellowHealth { get; private set; }
    public float blueHealth { get; private set; }

    public void InitializeHealth(PaintColor color)
    {
        
    }

    public void Damage(float amt, PaintColor color)
    {
        /*
        health = Mathf.Max(0, health - amt);

        OnEnemyDamageTaken?.Invoke(amt);

        if (health <= 0)
        {
            OnEnemyDeath?.Invoke(amt);
        }
        */
    }
}