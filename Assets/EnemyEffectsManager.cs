using UnityEngine;

public class EnemyEffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject damageTakenImpactEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDamageTakenImpactEffect(float amt, PaintColor color)
    {
        GameObject damageImpact = Instantiate(damageTakenImpactEffect, transform.position, transform.rotation);
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(color);
    }

    public void TriggerBigDamageTakenImpactEffect(float amt, PaintColor color)
    {
        GameObject damageImpact = Instantiate(damageTakenImpactEffect, transform.position, transform.rotation);
        damageImpact.GetComponent<EnemyDamageTakenImpact>().Explode(color, 2f);
    }
}
