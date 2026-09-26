using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;

    Vector2 playerPos;

    void Awake()
    {
        playerPos = transform.position;
    }

    public void SpawnBullet(Vector2 pos)
    {
        Vector2 dir = (pos - playerPos).normalized;

        Instantiate(bullet, playerPos, transform.rotation).GetComponent<PlayerProjectile>().Init(dir);
    }
}