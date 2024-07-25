using System;
using System.Collections.Generic;
using Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Feature.Resource
{
    [Serializable]
    public class ResourceSave : SaveData
    {
        [ShowInInspector]
        public Dictionary<EResource, int> ResourceDictionary = new Dictionary<EResource, int>();

        public Action<EResource, int> OnResourceChanged;

        public ResourceSave(string keySave) : base(keySave) { }

        public void SetResource(EResource type, int value)
        {
            var preChangeValue = GetResource(type);
            ResourceDictionary[type] = value;
            OnResourceChanged?.Invoke(type, value - preChangeValue);
        }

        public void AddResource(EResource type, int value)
        {
            if (ResourceDictionary.ContainsKey(type))
            {
                ResourceDictionary[type] += value;
            }
            else
            {
                ResourceDictionary.Add(type, value);
            }

            OnResourceChanged?.Invoke(type, value);
        }

        public void RemoveResource(EResource type, int value)
        {
            if (ResourceDictionary.ContainsKey(type))
            {
                var preChangeValue = GetResource(type);
                ResourceDictionary[type] = Mathf.Max(0, preChangeValue - value);
                OnResourceChanged?.Invoke(type, GetResource(type) - preChangeValue);
            }
        }

        public int GetResource(EResource type)
        {
            return ResourceDictionary.TryGetValue(type, out var value) ? value : 0;
        }
    }
}