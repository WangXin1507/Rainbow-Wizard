using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Bucket
{
    public class BucketInteraction : MonoBehaviour
    {
        public BucketData data;
        public Animator pourBucketAnim;
        public SpriteRenderer bucketPaintVisual;
        public SpriteRenderer pourColorVisual;
        public GameObject splatExplosion;
        Bucket bucket;
        bool pouring;

        public void SpawnBucketBomb()
        {
            if (pouring)
            {
                return;
            }

            PourBucket().Forget();
        }

        async UniTask PourBucket()
        {
            bucket.ToggleBucketVisibility(false);

            // spawn bucket anim
            pouring = true;
            Vector2 origin = Player.PlayerInput.MouseWorldPosition;
            pourBucketAnim.transform.position = new(origin.x, origin.y + 2);
            pourBucketAnim.gameObject.SetActive(true);
            pourBucketAnim.Play(0, 0, 0);

            // damage enemy in area, damage is calculated by finding how full the bucket is, then multiplied by
            // the percentage fill taken up by that color
            var paintContent = bucket.paintContent;
            foreach (var color in paintContent.Keys)
            {
                float damage = Mathf.Lerp(data.minDamage, data.maxDamage, bucket.CurrentFill / data.maxCapacity);
                damage *= (paintContent[color] / bucket.CurrentFill);
                DamageEnemiesInRadius(origin, damage, color.paintColor);

                ApplyLingerDamage(origin, damage, color.paintColor).Forget();
            }

            SetPainPourColor(bucket.MajorityPaint.color);
            Instantiate(splatExplosion, origin, transform.rotation).GetComponent<SplatExplosion>().Init(0, data.impactRadius, data.lingerDuration * 2, bucket.MajorityPaint.color, 0.5f);

            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.WaitUntil(
                () => pourBucketAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f,
                cancellationToken: token,
                cancelImmediately: true);

            pourBucketAnim.gameObject.SetActive(false);

            bucket.ToggleBucketVisibility(true);
            bucket.EmptyBucket();
            pouring = false;
        }

        async UniTask ApplyLingerDamage(Vector2 origin, float impactDamage, PaintColor color)
        {
            var token = this.GetCancellationTokenOnDestroy();
            int iteration = Mathf.FloorToInt(data.lingerDuration / data.lingerDamageFrequency);
            float dps = impactDamage * data.lingerDamagePerSecondMulti * data.lingerDamageFrequency;

            for (int i = 0; i < iteration; i ++)
            {
                DamageEnemiesInRadius(origin, dps, color);
                await UniTask.Delay(TimeSpan.FromSeconds(data.lingerDamageFrequency), cancellationToken: token);
            }
        }

        void DamageEnemiesInRadius(Vector2 origin, float baseDamage, PaintColor color)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(origin, data.impactRadius);
            foreach (Collider2D hit in hits)
            {
                if (!hit.TryGetComponent(out EnemyHealth health) || !health.IsAlive)
                {
                    continue;
                }

                float edgeT = Mathf.Clamp01(Vector2.Distance(origin, hit.transform.position) / data.impactRadius);
                float multi = Mathf.Lerp(data.centerDamageMultiplier, data.edgeDamageMultiplier, edgeT);
                health.Damage(baseDamage * multi, color);
            }
        }

        void SetPainPourColor(Color c)
        {
            bucketPaintVisual.color = pourColorVisual.color = c;
        }

        void Awake()
        {
            bucket = GetComponent<Bucket>();
        }
    }
}