using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// Sub to OnEnemyDamageTaken, OnEnemyDeath. 
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public UnityEvent<float, PaintColor> OnEnemyDamageTaken;
    public UnityEvent<float, PaintColor> OnEnemyDeath;

    [SerializeField] private float minMaxHealth = 90f;
    [SerializeField] private float maxMaxHealth = 150f;

    public float RedHealth { get; private set; }
    public float YellowHealth { get; private set; }
    public float BlueHealth { get; private set; }

    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    private void Awake()
    {
        RedHealth = 0f;
        YellowHealth = 0f;
        BlueHealth = 0f;
        IsAlive = true;
    }

    public void InitializeHealth(PaintColor color, float enemyWeight)
    {
        float randomHealth = Mathf.Lerp(minMaxHealth, maxMaxHealth, enemyWeight);
        randomHealth = Mathf.Floor(randomHealth);
        MaxHealth = randomHealth;
        if (PaintColorUtil.ContainsRed(color))
        {
            RedHealth = randomHealth;
        }
        if (PaintColorUtil.ContainsYellow(color))
        {
            YellowHealth = randomHealth;
        }
        if (PaintColorUtil.ContainsBlue(color))
        {
            BlueHealth = randomHealth;
        }
    }

    public void Damage(float amt, PaintColor color)
    {
        if (PaintColorUtil.ContainsRed(color))
        {
            RedHealth = Mathf.Max(0f, RedHealth - amt);
        }
        if (PaintColorUtil.ContainsBlue(color))
        {
            BlueHealth = Mathf.Max(0f, BlueHealth - amt);
        }
        if (PaintColorUtil.ContainsYellow(color))
        {
            YellowHealth = Mathf.Max(0f, YellowHealth - amt);
        }
        OnEnemyDamageTaken?.Invoke(amt, color);

        if (!HasHealth())
        {
            OnEnemyDeath?.Invoke(amt, color);
            IsAlive = false;
        }
    }

    public bool HasHealth()
    {
        if (RedHealth > 0f || YellowHealth > 0f || BlueHealth > 0f)
        {
            return true;
        }
        return false;
    }

}