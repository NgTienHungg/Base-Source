using Base.Core;
using UnityEngine;

namespace Base.Data
{
    public class DataManager : LiveSingleton<DataManager>
    {
        public static DatabaseManager Database => Instance.database;
        public static DatasaveManager Datasave => Instance.datasave;

        [SerializeField]
        private DatabaseManager database;

        [SerializeField]
        private DatasaveManager datasave;

        protected override void OnAwake() {
            OnInit();
        }

        private void OnInit() {
            database.Init();
            datasave.Init();
        }
    }
}