using PlayerPaint;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject bullet;
    Vector2 playerPos;
    float lastFiredTime;

    void Awake()
    {
        playerPos = transform.position;
    }

    public void SpawnBullet(Vector2 screenPos)
    {
        if (Player.PaintPool.ActivePaint.IsExhausted)
        {
            return;
        }

        if (Time.time - lastFiredTime < playerData.fireInterval)
        {
            return;
        }

        Vector2 dir = (screenPos - playerPos).normalized;

        Instantiate(bullet, playerPos, Quaternion.identity).GetComponent<PlayerProjectile>().Init(dir);

        Player.PaintPool.ActivePaint.UpdatePaintReserve(-playerData.paintCost);

        lastFiredTime = Time.time;
    }
}