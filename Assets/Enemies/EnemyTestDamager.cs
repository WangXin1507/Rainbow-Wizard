using UnityEngine;

public class EnemyTestDamager : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private PaintColor damageColor;

    [SerializeField] private bool dealDamage = false;

    private EnemyHealth enemyHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dealDamage) {
            dealDamage = false;
            enemyHealth.Damage(damageAmount, damageColor);
        }
    }
}
