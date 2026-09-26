using PlayerPaint;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace UI
{
    [RequireComponent(typeof(Scrollbar))]
    public class PaintResourceBar : UIElement
    {
        Scrollbar resourceBar;
        [HideInInspector]
        public PaintResource resource;

        bool exhaustVisualActive;

        public override void Awake()
        {
            resourceBar = GetComponent<Scrollbar>();
            base.Awake();
        }

        public void Initialize(PaintResource resource)
        {
            this.resource = resource;
            resource.OnPaintResourceChange.AddListener(OnPaintResourceUpdate);
        }

        private void OnDestroy()
        {
            if (resource)
            {
                resource.OnPaintResourceChange.RemoveListener(OnPaintResourceUpdate);
            }
        }

        public void OnPaintResourceUpdate(PaintResource resource, float amount)
        {
            resourceBar.size = amount;

            if (resource.IsExhausted)
            {
                OnResourceExhausted().Forget();
            }
        }

        private async UniTask OnResourceExhausted()
        {
            if (exhaustVisualActive) return;

            exhaustVisualActive = true;

            var colors = resourceBar.colors;
            colors.normalColor = Color.white;
            resourceBar.colors = colors;

            try
            {
                await UniTask.WaitUntil(
                    () => !resource.IsExhausted,
                    cancellationToken: this.GetCancellationTokenOnDestroy(),
                    cancelImmediately: true);

                colors = resourceBar.colors;
                colors.normalColor = resource.color;
                resourceBar.colors = colors;
            }
            finally
            {
                exhaustVisualActive = false;
            }
        }
    }
}