using Base.Architecture;
using UnityEngine;

namespace Base.Core
{
    public abstract class Service : IService
    {
        public void OnStart() {
            Debug.Log($"[Service] {GetType()} on start".Color("orange"));
        }

        public void OnInit() {
            Debug.Log($"[Service] {GetType()} on init".Color("lime"));
        }

        public void OnUpdate() { }

        public void OnDispose() {
            Debug.Log($"[Service] {GetType()} on dispose".Color("red"));
        }
    }
}