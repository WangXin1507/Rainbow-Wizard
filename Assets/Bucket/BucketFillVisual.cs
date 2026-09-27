using PlayerPaint;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Bucket {
    public class BucketFillVisual : MonoBehaviour
    {
        public Image red;
        public Image yellow;
        public Image blue;
        public Image orange;
        public Image purple;
        public Image green;
        public BucketData bucketData;
        public Bucket bucket;

        public const float MaxFill = 98;
        float currFill = 0;


        List<Image> allFills;

        void Awake()
        {
            allFills = new List<Image>
            {
                red,
                yellow,
                blue,
                orange,
                purple,
                green
            };

            foreach (var item in allFills)
            {
                item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
        }

        private void OnEnable()
        {
            bucket.OnAddPaintToBucket.AddListener(SetBucketFillVisual);
            bucket.OnBucketEmpty.AddListener(EmptyBucketVisual);
        }

        private void OnDisable()
        {
            bucket.OnAddPaintToBucket.RemoveListener(SetBucketFillVisual);
            bucket.OnBucketEmpty.RemoveListener(EmptyBucketVisual);
        }

        public void SetBucketFillVisual(PaintResource resource, float amount)
        {
            if (currFill == MaxFill)
            {
                return;
            }

            Image image = GetPaintColorImage(resource.paintColor);
            float curHeight = image.rectTransform.rect.height;
            float amtToAdd = Mathf.Lerp(0, MaxFill, (amount / bucketData.maxCapacity));

            currFill += amtToAdd;

            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, curHeight + amtToAdd);
        }

        public void EmptyBucketVisual()
        {
            currFill = 0;
            foreach (var item in allFills)
            {
                item.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            }
        }

        public Image GetPaintColorImage(PaintColor color)
        {
            return color switch
            {
                PaintColor.RED => red,
                PaintColor.ORANGE => orange,
                PaintColor.YELLOW => yellow,
                PaintColor.GREEN => green,
                PaintColor.BLUE => blue,
                PaintColor.PURPLE => purple,
                _ => blue,
            };
        }
    }
}