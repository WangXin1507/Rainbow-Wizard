using PlayerPaint;
using PrimeTween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bucket
{
    public class Bucket : MonoBehaviour
    {
        public UnityEvent<PaintResource, float> OnAddPaintToBucket;
        public UnityEvent OnBucketEmpty;

        public SpriteRenderer bucketPaintVisuals;
        public SpriteRenderer bucketVisuals;
        public BucketData data;
        public Dictionary<PaintResource, float> paintContent;
        public float CurrentFill => currentFill;
        public PaintResource MajorityPaint => majorityPaint.r;

        const float HoverScale = 1.15f;
        const float HoverDuration = 0.15f;

        float currentFill;
        (PaintResource r, float f) majorityPaint;
        Collider2D bucketCollider;
        BucketInteraction bucketInteraction;
        Vector3 paintRestScale;
        Vector3 bucketRestScale;
        Vector3 paintRestPos;
        Vector3 bucketRestPos;
        Tween paintTween;
        Tween bucketTween;
        bool hovered;
        bool dragging;
        bool rightMouseWasHeld;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!hovered || currentFill == data.maxCapacity)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out PlayerProjectile projectile))
            {
                currentFill = Mathf.Min(currentFill + data.fillPerShot, data.maxCapacity);

                if (paintContent.ContainsKey(projectile.paintResource))
                {
                    paintContent[projectile.paintResource] = paintContent[projectile.paintResource] + data.fillPerShot;
                }
                else
                {
                    paintContent[projectile.paintResource] = data.fillPerShot;
                }

                if (paintContent[projectile.paintResource] > majorityPaint.f)
                {
                    majorityPaint.r = projectile.paintResource;
                    majorityPaint.f = paintContent[projectile.paintResource];
                    SetBucketColor(majorityPaint.r);
                }

                OnAddPaintToBucket?.Invoke(projectile.paintResource, data.fillPerShot);

                Destroy(collision.gameObject);
            }
        }

        void Update()
        {
            if (Player.PlayerInput == null)
                return;

            bool rightHeld = Player.PlayerInput.RightMouse;
            bool rightPressed = rightHeld && !rightMouseWasHeld;
            bool rightReleased = !rightHeld && rightMouseWasHeld;
            rightMouseWasHeld = rightHeld;

            if (dragging)
            {
                MoveVisualsTo(Player.PlayerInput.MouseWorldPosition);

                if (rightReleased)
                {
                    dragging = false;
                    ResetVisuals();
                    bucketInteraction.SpawnBucketBomb();
                }
                return;
            }

            if (!bucketCollider.enabled)
            {
                if (hovered)
                {
                    hovered = false;
                    if (majorityPaint.f > 0)
                        ScaleVisuals(1f);
                }
                return;
            }

            bool isOver = bucketCollider.OverlapPoint(Player.PlayerInput.MouseWorldPosition);

            if (isOver && !hovered)
            {
                hovered = true;
                ScaleVisuals(HoverScale);
            }
            else if (!isOver && hovered)
            {
                hovered = false;
                ScaleVisuals(1f);
            }

            if (isOver && rightPressed && majorityPaint.f > 0)
            {
                hovered = false;
                paintTween.Stop();
                bucketTween.Stop();
                bucketPaintVisuals.transform.localScale = paintRestScale;
                bucketVisuals.transform.localScale = bucketRestScale;
                dragging = true;
                MoveVisualsTo(Player.PlayerInput.MouseWorldPosition);
            }
        }

        void Awake()
        {
            paintContent = new();
            bucketCollider = GetComponent<Collider2D>();
            bucketInteraction = GetComponent<BucketInteraction>();
            paintRestScale = bucketPaintVisuals.transform.localScale;
            bucketRestScale = bucketVisuals.transform.localScale;
            paintRestPos = bucketPaintVisuals.transform.localPosition;
            bucketRestPos = bucketVisuals.transform.localPosition;
        }

        void MoveVisualsTo(Vector2 world)
        {
            Vector3 paintPos = world;
            paintPos.z = bucketPaintVisuals.transform.position.z;
            bucketPaintVisuals.transform.position = paintPos;

            Vector3 bucketPos = world;
            bucketPos.z = bucketVisuals.transform.position.z;
            bucketVisuals.transform.position = bucketPos;
        }

        void ResetVisuals()
        {
            bucketPaintVisuals.transform.localPosition = paintRestPos;
            bucketVisuals.transform.localPosition = bucketRestPos;
            bucketPaintVisuals.transform.localScale = paintRestScale;
            bucketVisuals.transform.localScale = bucketRestScale;
        }

        void ScaleVisuals(float scale)
        {
            paintTween.Stop();
            bucketTween.Stop();
            paintTween = Tween.Scale(bucketPaintVisuals.transform, paintRestScale * scale, HoverDuration, Ease.OutSine);
            bucketTween = Tween.Scale(bucketVisuals.transform, bucketRestScale * scale, HoverDuration, Ease.OutSine);
        }

        public void SetBucketColor(PaintResource pr)
        {
            bucketPaintVisuals.color = pr.color;
        }

        public void ToggleBucketVisibility(bool enable)
        {
            bucketPaintVisuals.enabled = enable;
            bucketVisuals.enabled = enable;
            bucketCollider.enabled = enable;
        }

        public void EmptyBucket()
        {
            currentFill = 0;
            paintContent.Clear();
            majorityPaint = new();
            bucketPaintVisuals.color = Color.clear;

            OnBucketEmpty?.Invoke();
        }
    }
}