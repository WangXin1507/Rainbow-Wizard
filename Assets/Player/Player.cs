using UnityEngine;


/// <summary>
/// Use to access player components
/// </summary>
[DefaultExecutionOrder(-10000)]
[RequireComponent(typeof(PlayerHealth))]
//[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PaintPool))]
public class Player : MonoBehaviour
{
    public static PlayerHealth PlayerHealth;
    //public static PlayerInput PlayerInput;

    void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        //PlayerInput = GetComponent<PlayerInput>();
    }
}
