using UnityEngine;

public class EnemySpriteManager : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer frontSpriteRenderer;

    [SerializeField] private float minScale = 0.9f;
    [SerializeField] private float maxScale = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.position.x < 0f)
        {
            spriteRenderer.flipX = true;
            frontSpriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSpriteManager(PaintColor color, float enemyWeight)
    {
        Color colorColor = PaintColorUtil.GetPaintColorColor(color);
        spriteRenderer.color = colorColor;

        float scaleComponent = Mathf.Lerp(minScale, maxScale, enemyWeight);
        spriteRenderer.transform.localScale = new Vector3(scaleComponent, scaleComponent, scaleComponent);
        frontSpriteRenderer.transform.localScale = new Vector3(scaleComponent, scaleComponent, scaleComponent);
    }

    public void SetRenderOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
        frontSpriteRenderer.sortingOrder = order;
    }
}
