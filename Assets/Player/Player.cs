using UnityEngine;
using PlayerPaint;


/// <summary>
/// Use to access player components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PaintPool))]
[RequireComponent(typeof(PlayerFire))]
public class Player : MonoBehaviour
{
    public static PlayerHealth PlayerHealth;
    public static PlayerInput PlayerInput;
    public static PaintPool PaintPool;
    public static PlayerFire PlayerFire;

    void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
        PaintPool = GetComponent<PaintPool>();
        PlayerFire = GetComponent<PlayerFire>();
    }
}
