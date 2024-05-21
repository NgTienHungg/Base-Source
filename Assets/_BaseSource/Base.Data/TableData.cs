using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Base.Data
{
    [Serializable]
    public abstract class TableData<T, E> : ITableData
    {
        [ShowInInspector]
        public Dictionary<T, E> Dictionary = new Dictionary<T, E>();

        public bool Has(T key) {
            return Dictionary.ContainsKey(key);
        }

        public E Get(T key) {
            return Has(key) ? Dictionary[key] : default;
        }

        public virtual void Clear() {
            Dictionary = new Dictionary<T, E>();
        }

        public abstract void Fetch();
    }
}