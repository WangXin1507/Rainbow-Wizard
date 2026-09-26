using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace PlayerPaint
{
    [CreateAssetMenu(fileName = "CompositePaintResource", menuName = "Scriptable Objects/Composite Paint Resource Data")]
    public class CompositePaintResource : PaintResource
    {
        public List<PaintResource> childResources;
        public int ChildCount => childResources.Count;

        public override float Reserve => childResources.Sum(resource => resource.Reserve);
        public override bool IsExhausted => childResources.Any(resource => resource.IsExhausted);

        public override void UpdatePaintReserve(float amt)
        {
            foreach (var resource in childResources)
            {
                resource.UpdatePaintReserve(amt);
            }
        }

    }
}