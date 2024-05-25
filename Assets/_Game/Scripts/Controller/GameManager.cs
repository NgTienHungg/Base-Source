using Base.Core;
using UnityEngine;

namespace Controller
{
    public class GameManager : LiveSingleton<GameManager>
    {
        public int Gold { get; set; }
        public bool IsAdsRemoved { get; set; }

        protected override void OnAwake() {
            Application.targetFrameRate = 90;
        }
    }
}