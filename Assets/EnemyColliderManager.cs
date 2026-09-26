using UnityEngine;

public class EnemyColliderManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D enemyCollider;

    [SerializeField] private float minScale = 0.9f;
    [SerializeField] private float maxScale = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeSpriteManager(PaintColor color, float enemyWeight)
    {
        float scaleComponent = Mathf.Lerp(minScale, maxScale, enemyWeight);
        enemyCollider.size.Set(enemyCollider.size.x * scaleComponent, enemyCollider.size.y * scaleComponent);
    }
}
