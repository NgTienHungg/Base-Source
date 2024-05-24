using System.Collections.Generic;
using UnityEngine;

#if GAME_FEATURE
using Feature.Offer;
#endif

namespace Base.Data
{
    public class DatabaseManager : MonoBehaviour
    {
        #region ========== GAME DATABASE ==========
        public OfferTable Offer = new OfferTable();
        #endregion

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

            database.Clear();
            database.Add(Offer);

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