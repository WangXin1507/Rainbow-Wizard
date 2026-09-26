using UnityEngine;

/// <summary>
/// Use to access enemy components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemySpriteColorer))]
public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public EnemySpriteColorer spriteColorer;

    public PaintColor color = PaintColor.NONE;

    public bool isInitialized = false;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteColorer = GetComponent<EnemySpriteColorer>();
    }

    private void Start()
    {
        if (color != PaintColor.NONE)
        {
            InitializeEnemy();
        }
    }

    public void InitializeEnemy()
    {
        if (isInitialized) return;
        isInitialized = true;
        enemyHealth.InitializeHealth(color);
        spriteColorer.InitializeSpriteColorer(color);
    }
}
