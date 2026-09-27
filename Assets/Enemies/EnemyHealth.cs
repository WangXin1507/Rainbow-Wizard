using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

/// <summary>
/// Sub to OnEnemyDamageTaken, OnEnemyDeath. 
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public UnityEvent<float, PaintColor> OnEnemyDamageTaken;
    public UnityEvent<float, PaintColor> OnEnemyDamageIgnored;
    public UnityEvent<float, PaintColor> OnEnemyDeath;

    [SerializeField] private float minMaxHealth = 90f;
    [SerializeField] private float maxMaxHealth = 150f;

    [SerializeField] public int pointValue = 10;
    [SerializeField] public float pointDecayDelay = 4f;

    public float RedHealth { get; private set; }
    public float YellowHealth { get; private set; }
    public float BlueHealth { get; private set; }

    public float MaxHealth { get; private set; }
    public bool IsAlive { get; private set; }

    public bool IsDamaged { get; private set; }

    private void Awake()
    {
        RedHealth = 0f;
        YellowHealth = 0f;
        BlueHealth = 0f;
        IsAlive = true;
        IsDamaged = false;
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
        StartCoroutine(ReducePoints());
    }

    private IEnumerator ReducePoints()
    {
        while (IsAlive && pointValue > 1)
        {
            yield return new WaitForSeconds(pointDecayDelay);
            pointValue--;
        }
    }

    public void Damage(float amt, PaintColor color)
    {
        bool damageDealt = false;
        if (PaintColorUtil.ContainsRed(color) && RedHealth > 0f)
        {
            RedHealth = Mathf.Max(0f, RedHealth - amt);
            damageDealt = true;
        }
        if (PaintColorUtil.ContainsBlue(color) && BlueHealth > 0f)
        {
            BlueHealth = Mathf.Max(0f, BlueHealth - amt);
            damageDealt = true;
        }
        if (PaintColorUtil.ContainsYellow(color) && YellowHealth > 0f)
        {
            YellowHealth = Mathf.Max(0f, YellowHealth - amt);
            damageDealt = true;
        }

        if (damageDealt)
        {
            IsDamaged = true;
            OnEnemyDamageTaken?.Invoke(amt, color);
        }
        else
        {
            OnEnemyDamageIgnored?.Invoke(amt, color);
        }

        if (!HasHealth()) IsAlive = false;
        if (GetComponent<EnemyAttackManager>().isWindingUp)
        {
            if (!IsAlive) GetComponent<EnemyAudioManager>().StopAttackWindup();
            TryDie();
        }
    }

    public bool TryDie()
    {
        if (HasHealth()) return false;
        OnEnemyDeath?.Invoke(0f, GetComponent<Enemy>().color);
        IsAlive = false;
        EnemySpawner.Instance.DecrementEnemyCount();
        GameManager.Instance.points += pointValue;
        return true;
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