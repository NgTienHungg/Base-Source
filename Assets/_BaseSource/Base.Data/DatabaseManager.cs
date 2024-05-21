using System.Collections.Generic;
using UnityEngine;

namespace Base.Data
{
    public partial class DatabaseManager : MonoBehaviour
    {
        private readonly List<ITableData> database = new List<ITableData>();

        public void Init() {
            CollectTableData();
        }

        private void CollectTableData() {
            // find tables
            // var type = GetType();
            // var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // foreach (var property in properties) {
            //     if (typeof(ITableData).IsAssignableFrom(property.PropertyType)) {
            //         if (property.GetValue(this) is ITableData value) {
            //             Debug.Log("Add table: " + value.GetType().Name);
            //             database.Add(value);
            //         }
            //     }
            // }

            GetTables();

            // fetch data
            foreach (var table in database) {
                table.Clear();
                table.Fetch();
            }
        }

        public void Reload() {
            database.Clear();
            CollectTableData();
        }
    }
}