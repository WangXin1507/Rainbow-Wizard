using System;
using UnityEngine;
using UnityEngine.Events;

public class PaintResource : MonoBehaviour
{
    public UnityEvent<PaintResource, float> OnPaintResourceChange;
    public float Reserve => reserve;
    public bool IsExhausted => exhaustTimer > 0f;


    [Tooltip("Max capacity for this resource")]
    public float Capacity = 100f;
    public float RechargeDelay = 0.4f;
    public float RechargeSpeed = 15f;
    [Tooltip("Punishment cooldown for if player exhausts a paint resource")]
    public float ExhaustTimeOut = 1f;



    float reserve;
    float exhaustTimer;

    /// <summary>
    /// Modify the paint resource reserve by this amount
    /// </summary>
    public void UpdatePaintReserve(float amt)
    {
        reserve = Mathf.Clamp(reserve + amt, 0, Capacity);
        OnPaintResourceChange?.Invoke(this, reserve);

        if (reserve == 0)
        {
            Debug.Log($"Attempting to use more {this} resource than avaliable. Reserve: {reserve}, attempting to use: {amt}. Activating punishment.");

            exhaustTimer = ExhaustTimeOut;
        }
    }

    void Awake()
    {
        reserve = Capacity;
    }

    void Update()
    {
        exhaustTimer -= Time.deltaTime;
    }
}

public class Red : PaintResource { }

public class Yellow : PaintResource { }

public class Blue: PaintResource { }
