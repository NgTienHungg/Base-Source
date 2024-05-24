using System;
using UnityEngine.Serialization;

namespace Feature.Resource
{
    [Serializable]
    public class ResourceValue
    {
        public EResource resourceType;
        public int value;

        public ResourceValue(EResource resourceType, int value) {
            this.resourceType = resourceType;
            this.value = value;
        }
    }
}