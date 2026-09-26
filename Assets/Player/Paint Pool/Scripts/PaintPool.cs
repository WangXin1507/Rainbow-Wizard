using Sirenix.OdinInspector;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent<PaintResource> OnChangeActivePaint;

        [Header("Run time tool")]
        [ShowInInspector, ReadOnly] PaintResource activePaint;
        public PaintResource ActivePaint { get; private set; }

        public void SetActivePaint(PaintResource resource)
        {
            if (activePaint == resource)
            {
                return;
            }

            activePaint = resource;
            OnChangeActivePaint?.Invoke(activePaint);
        }

        void Awake()
        {
            foreach (var resource in resources)
            {
                resource.OnAwake(capacity, rechargeDelay, rechargeSpeed, exhaustTimeOut);
            }
            if (resources.Count > 0)
            {
                SetActivePaint(resources[0]);
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