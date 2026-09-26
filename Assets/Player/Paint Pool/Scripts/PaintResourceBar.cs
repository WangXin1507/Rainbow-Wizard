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

        const float MINFILL = 0.33f;
        const float MAXFILL = 1f;

        public override void Awake()
        {
            resourceBar = GetComponent<Scrollbar>();
            resource.OnPaintResourceChange.AddListener(OnPaintResourceUpdate);

            base.Awake();
        }

        void Start()
        {
            handle.color = resource.color;
            OnPaintResourceUpdate(resource, resource.Reserve);
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
            resourceBar.size = Mathf.Lerp(MINFILL, MAXFILL, amount);

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