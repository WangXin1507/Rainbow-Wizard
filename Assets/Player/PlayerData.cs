using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    public float health = 100f;
    public float fireInterval = 0.1f;

    [Header("Projectile Data")]
    public float impactDamage = 40f;
    public float initialSpeed = 5f;
    public float targetSpeed = 1f;
    public float maxColliderScale = 1.2f;
    public float timeToReachTargetSpeed = 7;
    public float paintCost = 15f;
    [Tooltip("Time it takes to detroy bullet after reaching target speed")]
    public float expirationTimer = 5f;

    [Header("Paint Pool")]
    [Tooltip("Max capacity for each resource")]
    public float capacity = 100f;
    public float rechargeDelay = 0.4f;
    public float rechargeSpeed = 15f;
    [Tooltip("Punishment cooldown for if player exhausts a paint resource")]
    public float exhaustTimeOut = 1f;
}