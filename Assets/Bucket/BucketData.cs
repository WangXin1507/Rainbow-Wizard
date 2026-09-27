using UnityEngine;

[CreateAssetMenu(fileName = "Bucket Data", menuName = "Scriptable Objects/Bucket Data")]
public class BucketData : ScriptableObject
{
    [Header("Filling the bucket")]
    [Tooltip("Max bucket fill")]
    public float maxCapacity = 100;
    [Tooltip("How much each hit contributes to the bucket fill")]
    public float fillPerShot = 15;

    [Header("Projectile stats")]
    [Tooltip("Damage when bucket only has one charge")] 
    public float minDamage = 10;
    [Tooltip("Damage when bucket is fully filled")]
    public float maxDamage = 100;
    public float centerDamageMultiplier = 1;
    public float edgeDamageMultiplier = 0.4f;
    public float impactRadius = 20;
    public float lingerDuration = 2;
    [Tooltip("Percentage of the original impact damage that should be delt over time in the impact area")]
    public float lingerDamagePerSecondMulti = 0.1f;
    [Tooltip("Only updates how often damage is calculated, does not affect DPS")]
    public float lingerDamageFrequency = 0.5f;
}