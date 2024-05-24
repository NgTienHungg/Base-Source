using Base.Core;
using UnityEngine;

namespace Controller
{
    public class GameManager : LiveSingleton<GameManager>
    {
        protected override void OnAwake() {
            Application.targetFrameRate = 90;
        }
    }
}