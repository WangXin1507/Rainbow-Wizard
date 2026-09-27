using UnityEngine;
using System.Collections.Generic;
using PrimeTween;
using Cysharp.Threading.Tasks;

public class SplatExplosion : MonoBehaviour
{
    public SpriteRenderer sp;
    public List<Sprite> splatts;

    public void Init(float startingRadius, float targetRadius, float duration, Color c, float starDelay = 0f)
    {
        sp.sprite = splatts[Random.Range(0, splatts.Count)];
        sp.color = c;
        sp.transform.localScale = new(startingRadius / 2, startingRadius / 2, startingRadius / 2);

        Tween.Custom(startingRadius, targetRadius, duration: 1f, startDelay: starDelay ,onValueChange: radius => 
        {
            radius /= 2;
            sp.transform.localScale = new(radius, radius, radius);
        });
        Tween.Alpha(sp, startValue: 1f, endValue: 0f, duration: duration);

        SelfDestruct(duration).Forget();
    }

    async UniTask SelfDestruct(float wait)
    {
        await UniTask.WaitForSeconds(wait);
        Destroy(gameObject);
    }
}