using Unity.Hierarchy;
using UnityEngine;

public class EnemyDamageTakenImpact : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float bigSplatterChance = 0.5f;
    [SerializeField] private float splatterAngleVariance = 60f;
    [SerializeField] private float splatterScaleVariance = 0.6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode(PaintColor color, float scale = 1f)
    {
        Scale(scale);
        Rotate();
        animator.SetTrigger("Explode");
        spriteRenderer.color = PaintColorUtil.GetPaintColorColor(color);
    }

    public void TryEarlyStop()
    {
        if (Random.value > bigSplatterChance) return;
        animator.speed = 0;
    }

    private void Scale(float scale)
    {
        float scaleComponent = Random.Range(scale - splatterScaleVariance, scale + splatterScaleVariance);
        transform.localScale = new Vector3(scaleComponent, scaleComponent, scaleComponent);
    }

    private void Rotate()
    {
        Quaternion baseRotation = Quaternion.LookRotation(Vector3.forward, transform.position);
        float randomVariance = Random.Range(-splatterAngleVariance, splatterAngleVariance);
        Quaternion varianceRotation = Quaternion.Euler(0f, 0f, randomVariance);
        transform.rotation = baseRotation * varianceRotation;
    }
}
