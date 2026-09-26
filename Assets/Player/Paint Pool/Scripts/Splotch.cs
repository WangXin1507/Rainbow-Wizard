using UnityEngine;
using UnityEngine.UI;

namespace PlayerPaint
{
    public class Splotch : MonoBehaviour
    {
        public Image img;

        void Awake()
        {
            Player.PaintPool.OnChangeActivePaint.AddListener(UpdateSplotchColor);            
        }

        public void UpdateSplotchColor(PaintResource resource)
        {
            img.color = resource.color;
        }
    }
}