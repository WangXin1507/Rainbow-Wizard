using Cysharp.Threading.Tasks;
using PlayerPaint;
using PrimeTween;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float impactDamage;
    public float initialSpeed;
    public float acceleration;
    public float targetSpeed;
    public Vector2 maxColliderScale;
    public float timeToReachTargetSpeed;
    [Tooltip("Time it takes to detroy bullet after reaching target speed")]
    public float expirationTimer;

    PaintResource paintResource;
    SpriteRenderer sr;
    Rigidbody2D rb;
    BoxCollider2D col;

    Vector2 direction;
    float currentSpeed;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        currentSpeed = initialSpeed;
        transform.right = direction;
        rb.linearVelocity = direction * currentSpeed;

        Tween.Custom(this, initialSpeed, targetSpeed, timeToReachTargetSpeed, (proj, speed) =>
        {
            proj.currentSpeed = speed;
            proj.rb.linearVelocity = proj.direction * speed;
        });

        Tween.Custom(this, col.size, maxColliderScale, timeToReachTargetSpeed, (proj, size) =>
        {
            proj.col.size = size;
        });

        Lifetime().Forget();
    }

    async UniTask Lifetime()
    {
        var token = this.GetCancellationTokenOnDestroy();

        await UniTask.WaitUntil(
            () => Mathf.Abs(currentSpeed - targetSpeed) <= 0.001f,
            cancellationToken: token,
            cancelImmediately: true);

        await UniTask.WaitForSeconds(
            expirationTimer,
            cancellationToken: token,
            cancelImmediately: true);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.TryGetComponent())    
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        paintResource = Player.PaintPool.ActivePaint;
        sr.color = paintResource.color;
    }
}