using System;
using System.Collections;
using UnityEngine;

namespace Base
{
    public static class TimeHelper
    {
        public static IEnumerator CountDown(int duration, Action<int> onTick, Action onEnd, int interval = 1) {
            var remainingTime = duration;
            var waitTime = new WaitForSeconds(interval);

            while (remainingTime > 0) {
                onTick?.Invoke(remainingTime);
                yield return waitTime;
                remainingTime -= interval;

                if (remainingTime == 0)
                    onTick?.Invoke(remainingTime);
            }

            onEnd?.Invoke();
        }

        public static IEnumerator CountUp(int duration, Action<int> onTick, Action onEnd, int interval = 1) {
            var elapsedTime = 0;
            var waitTime = new WaitForSeconds(interval);

            while (elapsedTime < duration) {
                onTick?.Invoke(elapsedTime);
                yield return waitTime;
                elapsedTime++;

                if (elapsedTime == duration)
                    onTick?.Invoke(elapsedTime);
            }

            onEnd?.Invoke();
        }
    }
}