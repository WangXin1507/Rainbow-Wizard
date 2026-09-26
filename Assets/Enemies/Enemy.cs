using UnityEngine;

/// <summary>
/// Use to access enemy components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemySpriteManager))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAnimationManager))]
public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public EnemySpriteManager spriteColorer;
    public EnemyMovement enemyMovement;
    public EnemyAnimationManager animationManager;

    public PaintColor color = PaintColor.NONE;
    public float enemyWeight = -1f;

    public bool isInitialized = false;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteColorer = GetComponent<EnemySpriteManager>();
        enemyMovement = GetComponent<EnemyMovement>();
        animationManager = GetComponent<EnemyAnimationManager>();
        if (enemyWeight < 0f)
        {
            enemyWeight = Random.value;
        }
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
        enemyHealth.InitializeHealth(color, enemyWeight);
        spriteColorer.InitializeSpriteManager(color, enemyWeight);
        enemyMovement.InitializeEnemyMovement(enemyWeight);
        animationManager.InitializeAnimationManager(enemyWeight);
    }
}
