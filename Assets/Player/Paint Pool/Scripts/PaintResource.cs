using UnityEngine;
using UnityEngine.Events;

namespace PlayerPaint
{
    [CreateAssetMenu(fileName = "PaintResource", menuName = "Scriptable Objects/Paint Resource Data")]
    public class PaintResource : ScriptableObject
    {
        public UnityEvent<PaintResource, float> OnPaintResourceChange;
        public virtual float Reserve => reserve;
        public virtual bool IsExhausted => exhaustTimer > 0f;
        public PaintColor paintColor = PaintColor.NONE;
        public Color color;

        float capacity;
        float rechargeDelay;
        float rechargeSpeed;
        float exhaustTimeOut;

        float reserve;
        float exhaustTimer;
        float rechargeTimer;

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

            OnPaintResourceChange?.Invoke(this, reserve / capacity);
        }

        public void OnAwake(float capacity, float rechargeDelay, float rechargeSpeed, float exhaustTimeOut)
        {
            this.capacity = capacity;
            this.rechargeDelay = rechargeDelay;
            this.rechargeSpeed = rechargeSpeed;
            this.exhaustTimeOut = exhaustTimeOut;

            reserve = capacity;

            Player.PlayerFire.OnPlayerFiresProjectile.AddListener(RefreshRechargeDelay);
        }

        public void OnUpdate(float deltaT)
        {
            exhaustTimer -= deltaT;
            rechargeTimer -= deltaT;

            if (rechargeTimer < 0)
            {
                UpdatePaintReserve(rechargeSpeed * deltaT);
            }
        }

        public void RefreshRechargeDelay(GameObject bullet)
        {
            if (Player.PaintPool.ActivePaint == this)
            {
                rechargeTimer = rechargeDelay;
            }
        }
    }
}