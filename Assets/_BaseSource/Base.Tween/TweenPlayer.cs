using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.Tween
{
    public class TweenPlayer : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly]
        private List<ITween> uiTweens = new List<ITween>();

        public void Init() {
            uiTweens = GetComponentsInChildren<ITween>(includeInactive: true).ToList();
            uiTweens.ForEach(e => e.Init());
        }

        public async UniTask ShowTween(CancellationToken token) {
            var listTask = uiTweens.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Show())
                .ToList();

            await UniTask.WhenAll(listTask).AttachExternalCancellation(token);
        }

        public async UniTask HideTween(CancellationToken token) {
            var listTask = uiTweens.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Hide())
                .ToList();

            await UniTask.WhenAll(listTask).AttachExternalCancellation(token);
        }
    }
}