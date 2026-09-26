using System.Collections.Generic;
using UI;
using UnityEngine;

namespace PlayerPaint
{
    public class PaintPool : MonoBehaviour
    {
        public List<PaintResource> resources;

        [SerializeField] Canvas resourceUI;
        [SerializeField] List<PaintResourceBar> resourceBars;


        void Awake()
        {
        }
    }
}