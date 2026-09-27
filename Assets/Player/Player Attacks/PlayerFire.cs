using UnityEngine;
using UnityEngine.Events;

namespace PlayerPaint
{
    public class PlayerFire : MonoBehaviour
    {
        public PlayerData playerData;
        public GameObject bullet;
        public Animator animator;

        [SerializeField] private GameObject playerCharacterColor;
        [SerializeField] private GameObject playerCharacter;

        public UnityEvent<PaintResource> OnPlayerFiresProjectile;

        Vector2 playerPos;
        float lastFiredTime;
        int attackTriggerHash;
        float facingDir = 1;

        void Awake()
        {
            playerPos = transform.position;
            attackTriggerHash = Animator.StringToHash("PlayerAttack");
        }

        public void SpawnBullet(Vector2 screenPos)
        {
            if (Player.PaintPool.ActivePaint.IsExhausted)
            {
                GameAudioManager.Instance.PlayNoAmmoShoot();
                return;
            }

            if (Time.time - lastFiredTime < playerData.fireInterval)
            {
                return;
            }

            animator.SetTrigger(attackTriggerHash);

            Vector2 dir = (screenPos - playerPos).normalized;

            FlipPlayerDirection(dir);

            Instantiate(bullet, playerPos, Quaternion.identity).GetComponent<PlayerProjectile>().Init(dir);

            Player.PaintPool.ActivePaint.UpdatePaintReserve(-playerData.paintCost);

            lastFiredTime = Time.time;
            OnPlayerFiresProjectile?.Invoke(Player.PaintPool.ActivePaint);
            GameAudioManager.Instance.PlayShoot();
        }

        public void FlipPlayerDirection(Vector2 dir)
        {
            if (facingDir * dir.x > 0)
            {
                return;
            }
            else
            {
                facingDir = -facingDir;
                playerCharacterColor.transform.Rotate(0, 180, 0, Space.Self);
                playerCharacter.transform.Rotate(0, 180, 0, Space.Self);
            }
        }
    }
}