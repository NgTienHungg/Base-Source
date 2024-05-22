using DG.Tweening;
using UnityEngine;

namespace Base.Tween
{
    [CreateAssetMenu(menuName = "HungNT/TweenSettings")]
    public class TweenSettings : ScriptableObject
    {
        [Space]
        public Ease curveIn = Ease.Linear;
        public Ease curveOut = Ease.Linear;

        [Space]
        public float durationIn = 0.2f;
        public float durationOut = 0.2f;
    }
}