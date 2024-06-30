using System.Collections.Generic;
using UnityEngine;

namespace Base.Data
{
    public partial class DatabaseManager : MonoBehaviour
    {
        private readonly List<ITableData> database = new List<ITableData>();

        public void Init() {
            FetchAllData();
        }

        private void FetchAllData() {
            // add tables in game
            CollectTables();

            // fetch data
            foreach (var table in database) {
                table.Clear();
                table.Fetch();
            }
        }

        public void Reload() {
            FetchAllData();
        }
    }
}