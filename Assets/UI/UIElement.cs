using PrimeTween;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour
    {
        [HideInInspector]
        public RectTransform rectTransform;
        [HideInInspector]
        public CanvasGroup canvasGroup;

        Tween tween;

        public virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = 1f;
        }

        public void Show(float time = 0f)
        {
            tween.Stop();
            gameObject.SetActive(true);
            SetInteractable(true);

            if (time <= 0f)
            {
                canvasGroup.alpha = 1f;
                return;
            }

            tween = Tween.Alpha(canvasGroup, 1f, time, useUnscaledTime: true);
        }

        public void Hide(float time = 0f)
        {
            tween.Stop();
            SetInteractable(false);

            if (time <= 0f)
            {
                canvasGroup.alpha = 0f;
                return;
            }

            tween = Tween.Alpha(canvasGroup, 0f, time, useUnscaledTime: true);
        }

        void SetInteractable(bool value)
        {
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}