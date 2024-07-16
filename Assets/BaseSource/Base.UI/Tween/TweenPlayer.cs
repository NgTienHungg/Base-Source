using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.UI
{
    public class TweenPlayer : MonoBehaviour
    {
        [ShowInInspector] [ReadOnly]
        private List<ITween> uiTweens = new List<ITween>();

        public UniTask Init()
        {
            uiTweens = GetComponentsInChildren<ITween>(includeInactive: true).ToList();
            return UniTask.WhenAll(uiTweens.Select(tween => tween.Init()));
        }

        public UniTask ShowTween(CancellationToken token)
        {
            var showTasks = uiTweens.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Show())
                .ToList();

            return UniTask.WhenAll(showTasks).AttachExternalCancellation(token);
        }

        public UniTask HideTween(CancellationToken token)
        {
            var hideTasks = uiTweens.Where(tween => tween.IsAutoRun)
                .Select(tween => tween.Hide())
                .ToList();

            return UniTask.WhenAll(hideTasks).AttachExternalCancellation(token);
        }
    }
}