using UnityEngine;

/// <summary>
/// Use to access enemy components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemySpriteManager))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAnimationManager))]
[RequireComponent(typeof(EnemyColliderManager))]
public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public EnemySpriteManager spriteColorer;
    public EnemyMovement enemyMovement;
    public EnemyAnimationManager animationManager;
    public EnemyColliderManager colliderManager;

    public PaintColor color = PaintColor.NONE;
    public float enemyWeight = -1f;

    public bool isInitialized = false;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        spriteColorer = GetComponent<EnemySpriteManager>();
        enemyMovement = GetComponent<EnemyMovement>();
        animationManager = GetComponent<EnemyAnimationManager>();
        colliderManager = GetComponent<EnemyColliderManager>();
        if (enemyWeight < 0f)
        {
            enemyWeight = Random.value;
        }
    }

    private void Start()
    {
        if (color != PaintColor.NONE)
        {
            InitializeEnemy(color);
        }
    }

    public void InitializeEnemy(PaintColor newColor)
    {
        if (isInitialized) return;
        isInitialized = true;

        color = newColor;
        enemyHealth.InitializeHealth(color, enemyWeight);
        spriteColorer.InitializeSpriteManager(color, enemyWeight);
        enemyMovement.InitializeEnemyMovement(enemyWeight);
        animationManager.InitializeAnimationManager(enemyWeight);
        colliderManager.InitializeColliderManager(enemyWeight);
    }
}
