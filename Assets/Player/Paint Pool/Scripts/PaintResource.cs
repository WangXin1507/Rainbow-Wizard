using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerPaint
{
    public class PaintResource : MonoBehaviour
    {
        public UnityEvent<PaintResource, float> OnPaintResourceChange;
        public virtual float Reserve => reserve;
        public virtual bool IsExhausted => exhaustTimer > 0f;


        [Tooltip("Max capacity for this resource")]
        public float capacity = 100f;
        public float rechargeDelay = 0.4f;
        public float rechargeSpeed = 15f;
        [Tooltip("Punishment cooldown for if player exhausts a paint resource")]
        public float exhaustTimeOut = 1f;
        public Color color;



        float reserve;
        float exhaustTimer;

        /// <summary>
        /// Modify the paint resource reserve by this amount
        /// </summary>
        public virtual void UpdatePaintReserve(float amt)
        {
            reserve = Mathf.Clamp(reserve + amt, 0, capacity);
            if (reserve == 0)
            {
                Debug.Log($"Attempting to use more {this} resource than avaliable. Reserve: {reserve}, attempting to use: {amt}. Activating punishment.");

                exhaustTimer = exhaustTimeOut;
            }

            OnPaintResourceChange?.Invoke(this, reserve);
        }

        void Awake()
        {
            reserve = capacity;
        }

        void Update()
        {
            exhaustTimer -= Time.deltaTime;
        }
    }

    public class CompositePaintResource : PaintResource
    {
        public List<PaintResource> childResources;
        public int ChildCount => childResources.Count;

        public override float Reserve => childResources.Sum(resource => resource.Reserve);
        public override bool IsExhausted => childResources.Any(resource => resource.IsExhausted);

        public override void UpdatePaintReserve(float amt)
        {
            if (amt > 0)
            {
                throw new ArgumentException("Cannot add directly to composite paint resource");
            }

            foreach (var resource in childResources)
            {
                resource.UpdatePaintReserve(amt);
            }
        }
    }

    public class Red : PaintResource { }

    public class Yellow : PaintResource { }

    public class Blue : PaintResource { }

    public class Orange : CompositePaintResource { }

    public class Purple : CompositePaintResource { }

    public class Green : CompositePaintResource { }
}