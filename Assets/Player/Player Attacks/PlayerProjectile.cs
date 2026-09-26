using Cysharp.Threading.Tasks;
using PlayerPaint;
using PrimeTween;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public PlayerData playerData;
    [HideInInspector] public PaintResource paintResource;
    SpriteRenderer sr;
    Rigidbody2D rb;
    CircleCollider2D col;

    Vector2 direction;
    float currentSpeed;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        currentSpeed = playerData.initialSpeed;
        transform.up = direction;
        rb.linearVelocity = direction * currentSpeed;

        Tween.Custom(this, playerData.initialSpeed, playerData.targetSpeed, playerData.timeToReachTargetSpeed, (proj, speed) =>
        {
            proj.currentSpeed = speed;
            proj.rb.linearVelocity = proj.direction * speed;
        });

        Tween.Custom(this, col.radius, playerData.maxColliderScale, playerData.timeToReachTargetSpeed, (proj, radius) =>
        {
            proj.col.radius = radius;
        });

        Lifetime().Forget();
    }

    async UniTask Lifetime()
    {
        var token = this.GetCancellationTokenOnDestroy();

        await UniTask.WaitUntil(
            () => Mathf.Abs(currentSpeed - playerData.targetSpeed) <= 0.001f,
            cancellationToken: token,
            cancelImmediately: true);

        await UniTask.WaitForSeconds(
            playerData.expirationTimer,
            cancellationToken: token,
            cancelImmediately: true);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyHealth health))
        {
            health.Damage(playerData.impactDamage, paintResource.paintColor);
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        paintResource = Player.PaintPool.ActivePaint;
        sr.color = paintResource.color;
    }
}