using PlayerPaint;
using UnityEngine;

public class ColorFrame : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        Player.PaintPool.OnChangeActivePaint.AddListener(UpdateSplotchColor);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSplotchColor(PaintResource resource)
    {
        Color frameColor = resource.color;
        frameColor.a = spriteRenderer.color.a;
        spriteRenderer.color = frameColor;
    }
}
