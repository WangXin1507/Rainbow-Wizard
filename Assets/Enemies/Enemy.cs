using UnityEngine;

/// <summary>
/// Use to access enemy components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public PaintColor color = PaintColor.NONE;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        if (color != PaintColor.NONE)
        {
            enemyHealth.InitializeHealth(color);
        }
    }
}
