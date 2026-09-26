using System.Collections.Generic;
using UI;
using UnityEngine;

namespace PlayerPaint
{
    public class PaintPool : MonoBehaviour
    {
        public List<PaintResource> resources;

        [Tooltip("Max capacity for each resource")]
        public float capacity = 100f;
        public float rechargeDelay = 0.4f;
        public float rechargeSpeed = 15f;
        [Tooltip("Punishment cooldown for if player exhausts a paint resource")]
        public float exhaustTimeOut = 1f;

        [SerializeField] Canvas resourceUI;
        [SerializeField] List<PaintResourceBar> resourceBars;


        void Awake()
        {
            foreach (var resource in resources)
            {
                resource.OnAwake(capacity, rechargeDelay, rechargeSpeed, exhaustTimeOut);
            }
        }

        void Update()
        {
            foreach (var resource in resources)
            {
                resource.OnUpdate(Time.deltaTime);
            }
        }
    }
}