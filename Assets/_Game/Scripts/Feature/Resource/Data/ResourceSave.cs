using System;
using System.Collections.Generic;
using Base.Core;
using Base.Data;
using UnityEngine;

namespace Feature.Resource
{
    [Serializable]
    public class ResourceSave : SaveData
    {
        public Action<EResource, int> OnResourceChanged;
        public Dictionary<EResource, int> Resources;

        public ResourceSave(string keySave) : base(keySave) {
            Resources = new Dictionary<EResource, int>();
        }

        public void SetResource(EResource type, int value) {
            var preChangeValue = GetResource(type);
            Resources[type] = value;
            OnResourceChanged?.Invoke(type, value - preChangeValue);
        }

        public void AddResource(EResource type, int value) {
            if (Resources.ContainsKey(type)) {
                Resources[type] += value;
            }
            else {
                Resources.Add(type, value);
            }

            OnResourceChanged?.Invoke(type, value);
        }

        public void RemoveResource(EResource type, int value) {
            if (Resources.ContainsKey(type)) {
                var preChangeValue = GetResource(type);
                Resources[type] = Mathf.Max(0, preChangeValue - value);
                OnResourceChanged?.Invoke(type, GetResource(type) - preChangeValue);
            }
        }

        public int GetResource(EResource type) {
            return Resources.ContainsKey(type) ? Resources[type] : 0;
        }
    }
}