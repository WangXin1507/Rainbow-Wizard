using UnityEditor.Animations;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] private float minAnimSpeed = 0.9f;
    [SerializeField] private float maxAnimSpeed = 1.5f;

    [SerializeField] private Animator animator;

    public float AnimSpeed { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAnimationManager(float enemyWeight)
    {
        AnimSpeed = Mathf.Lerp(maxAnimSpeed, minAnimSpeed, enemyWeight);
        animator.speed = AnimSpeed;
    }

    public void TriggerHitStunAnim()
    {
        animator.SetTrigger("Damaged");
    }

    public void TriggerDeathAnim()
    {
        animator.SetTrigger("Dead");
    }
}
