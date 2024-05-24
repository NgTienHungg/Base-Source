using System;
using Base.Core;
using Feature.Resource;
using UnityEngine;

namespace Controller
{
    public class GameManager : LiveSingleton<GameManager>
    {
        public int Gold { get; set; }

        protected override void OnAwake() {
            Application.targetFrameRate = 90;
        }
    }
}