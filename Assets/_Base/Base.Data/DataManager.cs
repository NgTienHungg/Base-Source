using System;
using UnityEngine;

namespace Base
{
    public class DataManager : LiveSingleton<DataManager>
    {
        public static DatabaseManager Database => Instance.database;
        public static DatasaveManager Datasave => Instance.datasave;

        // [SerializeField]
        private DatabaseManager database;

        // [SerializeField]
        private DatasaveManager datasave;

        //--------

        public static DataConfigManager DataConfig => Instance.dataConfig;

        public static DataSaveManager DataSave => Instance.dataSave;

        [SerializeField]
        private DataConfigManager dataConfig;

        [SerializeField]
        private DataSaveManager dataSave;

        protected override void OnAwake()
        {
            dataConfig.Init();
            dataSave.Init();
        }
    }
}