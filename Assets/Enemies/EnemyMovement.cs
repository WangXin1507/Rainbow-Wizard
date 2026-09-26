using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float minMoveSpeed = 0.9f;
    [SerializeField] private float maxMoveSpeed = 1.5f;
    [SerializeField] private float hitStunSlowMultiplier = 0.5f;

    private float minYPosition = 25f;
    private float maxYPosition = -25f;
    private float minZPosition = 0f;
    private float maxZPosition = -99f;

    private Vector3 moveDirection;
    private Vector3 zAdjustedPosition;

    private bool isHitStunned = false;
    private bool canMove = true;

    public float MoveSpeed { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        MoveSpeed = 0f;
        targetPosition.z = transform.position.z;
    }

    public void InitializeEnemyMovement(float enemyWeight)
    {
        MoveSpeed = Mathf.Lerp(maxMoveSpeed, minMoveSpeed, enemyWeight);
    }

    // Update is called once per frame
    void Update()
    {
        // Hit stun slow muliplier
        float speedMultiplier = 1f;
        if (isHitStunned) speedMultiplier -= hitStunSlowMultiplier;

        // Z adjustment based on Y position
        float yLerpValue = Mathf.InverseLerp(minYPosition, maxYPosition, transform.position.y);
        zAdjustedPosition = transform.position;
        zAdjustedPosition.z = Mathf.Lerp(minZPosition, maxZPosition, yLerpValue);
        transform.position = zAdjustedPosition;
        targetPosition.z = transform.position.z;

        // Move towards target
        if (!canMove) return;
        moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * MoveSpeed * speedMultiplier * Time.deltaTime;
    }

    public void TriggerHitStunSlow()
    {
        isHitStunned = true;
    }

    public void EndHitStunSlow()
    {
        isHitStunned = false;
    }

    public void StartMoving()
    {
        canMove = true;
    }

    public void StopMoving()
    {
        canMove = false;
    }
}
