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
        public PaintResource resource;
        public Image handle;

        bool exhaustVisualActive;

        public override void Awake()
        {
            resourceBar = GetComponent<Scrollbar>();
            resource.OnPaintResourceChange.AddListener(OnPaintResourceUpdate);

            handle.color = resource.color;

            base.Awake();
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

            handle.color = Color.white;

            try
            {
                await UniTask.WaitUntil(
                    () => !resource.IsExhausted,
                    cancellationToken: this.GetCancellationTokenOnDestroy(),
                    cancelImmediately: true);

                handle.color = resource.color;
            }
            finally
            {
                exhaustVisualActive = false;
            }
        }
    }
}