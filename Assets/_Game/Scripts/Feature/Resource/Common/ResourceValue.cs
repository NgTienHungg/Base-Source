using System;
using UnityEngine.Serialization;

namespace Feature.Resource
{
    [Serializable]
    public class ResourceValue
    {
        [FormerlySerializedAs("ResourceType")] public EResource resourceType;
        [FormerlySerializedAs("Value")] public int value;

        public ResourceValue(EResource resourceType, int value) {
            this.resourceType = resourceType;
            this.value = value;
        }
    }
}