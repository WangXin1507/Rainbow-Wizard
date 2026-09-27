using Cysharp.Threading.Tasks;
using PlayerPaint;
using UnityEngine;
using UnityEngine.UI;

public class HealthResourceBar : UI.UIElement
{
    Scrollbar resourceBar;
    public Image handle;

    const float MINFILL = 0.202f;
    const float MAXFILL = 1f;

    public override void Awake()
    {
        resourceBar = GetComponent<Scrollbar>();

        base.Awake();
    }

    void Start()
    {
        //UpdateValue(0f);
    }

    public void UpdateValue(float value, bool damaged)
    {
        resourceBar.size = Mathf.Lerp(MINFILL, MAXFILL, value);

        if ((value >= 1f && !damaged) || value <= 0f)
        {
            Hide();
            return;
        }
        Show();
    }

    public void UpdateColor(Color color)
    {
        handle.color = color;
    }
}
