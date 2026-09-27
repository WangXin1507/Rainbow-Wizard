using Sirenix.OdinInspector;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerPaint
{
    public class PaintPool : MonoBehaviour
    {
        public PlayerData playerData;

        public List<PaintResource> resources;

        [SerializeField] Canvas resourceUI;
        [SerializeField] List<PaintResourceBar> resourceBars;

        public UnityEvent<PaintResource> OnChangeActivePaint;

        [Header("Run time tool")]
        [ShowInInspector, ReadOnly] PaintResource activePaint;
        public PaintResource ActivePaint => activePaint;

        public void SetActivePaint(PaintResource resource)
        {
            if (Player.PlayerHealth.Health <= 0)
            {
                return;
            }

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
                resource.OnAwake(playerData.capacity, playerData.rechargeDelay, playerData.rechargeSpeed, playerData.exhaustTimeOut);
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