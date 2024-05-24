using Base.Core;
using UnityEngine;

namespace Controller
{
    public class GameManager : MonoSingleton<GameManager>
    {
        protected override void OnAwake() {
            Application.targetFrameRate = 90;
        }
    }
}