using Cysharp.Threading.Tasks;
using PrimeTween;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Star : UIElement
{
    public Image image;

    public override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();

        Hide();
    }

    public async UniTask Init()
    {
        Show();
        rectTransform.sizeDelta = new Vector2(500f, 500f);
        await Tween.UISizeDelta(rectTransform, new Vector2(300f, 300f), 0.35f, Ease.OutBack, useUnscaledTime: true);
    }
}
