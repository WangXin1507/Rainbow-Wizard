using PlayerPaint;
using UnityEngine;
using UnityEngine.UI;

public class ColorFrame : MonoBehaviour
{
    public Slider healthBar;
    public float iHateSlidersHeresTheEstimatedValueWhereTheSliderActuallyLooks100IDontWannaFigureItOut = 0.81f;
    public PlayerData playerData;
    [HideInInspector] public Image spriteRenderer;

    void Start()
    {
        Player.PaintPool.OnChangeActivePaint.AddListener(UpdateSplotchColor);
        Player.PlayerHealth.OnPlayerDamageTaken.AddListener(SyncHealthBarVisuals);
        SyncHealthBarVisuals(0, playerData.health);
        spriteRenderer = GetComponent<Image>();
    }

    public void UpdateSplotchColor(PaintResource resource)
    {
        Color frameColor = resource.color;
        frameColor.a = spriteRenderer.color.a;
        spriteRenderer.color = frameColor;
    }

    public void SyncHealthBarVisuals(float damageTaken, float updatedHealth)
    {
        if (healthBar == null)
        {
            return;
        }
        healthBar.value = Mathf.Lerp(0, iHateSlidersHeresTheEstimatedValueWhereTheSliderActuallyLooks100IDontWannaFigureItOut, updatedHealth / playerData.health);
    }
}
