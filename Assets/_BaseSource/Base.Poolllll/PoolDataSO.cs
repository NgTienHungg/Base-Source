using UnityEngine;
using System.Collections.Generic;
using System;

namespace Base.Pool
{
    [CreateAssetMenu(menuName = "GameData/PoolDataSO", fileName = nameof(PoolDataSO))]
    public partial class PoolDataSO : ScriptableObject
    {
        public List<GameObject> prefabs;
    }

    [Serializable]
    public partial class Pool
    {
        public string key;
        public int startSize;
    }
}