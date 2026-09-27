using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float minAttackDamage = 350f;
    [SerializeField] private float maxAttackDamage = 600f;

    public bool isWindingUp = false;
    public bool isAttacking = false;

    private float attackDamage = 0f;
    private Enemy enemy;
    //private EnemyHealth enemyHealth;
    //private EnemyAnimationManager animationManager;
    //private EnemyMovement enemyMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<Enemy>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //animationManager = GetComponent<EnemyAnimationManager>();
        //enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAttackManager(float enemyWeight)
    {
        attackDamage = Mathf.FloorToInt(Mathf.Lerp(minAttackDamage, maxAttackDamage, enemyWeight));
    }

    public void StartWindingUp()
    {
        isWindingUp = true;
        //enemyMovement.StopMoving();
        //animationManager.TriggerAttackAnim();
        enemy.enemyMovement.StopMoving();
        enemy.animationManager.TriggerAttackAnim();
    }

    public void ExecuteAttack()
    {
        Player.PlayerHealth.Damage(attackDamage);
        isWindingUp = false;
        isAttacking = true;
        //enemyHealth.Damage(99999f, PaintColor.ALL);
        enemy.colliderManager.DisableCollider();
        enemy.enemyHealth.Damage(99999f, PaintColor.ALL);
        enemy.effectsManager.TriggerBiggestDamageTakenImpactEffect(0f, enemy.color);
        EnemySpawner.Instance.DecrementEnemyCount();
    }
}
