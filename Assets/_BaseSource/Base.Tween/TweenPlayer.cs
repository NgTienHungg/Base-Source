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
        [ShowInInspector]
        private List<ITween> uiTween = new List<ITween>();

        public void Init() {
            uiTween = GetComponentsInChildren<ITween>(includeInactive: true).ToList();
            uiTween.ForEach(e => e.Init());
        }

        public async UniTask ShowTween(CancellationToken token) {
            var listTask = uiTween.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Show())
                .ToList();

            await UniTask.WhenAll(listTask).AttachExternalCancellation(token);
        }

        public async UniTask HideTween(CancellationToken token) {
            var listTask = uiTween.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Hide())
                .ToList();

            await UniTask.WhenAll(listTask).AttachExternalCancellation(token);
        }
    }
}