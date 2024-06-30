using Base.Data;
using UnityEngine;
using Zenject;

namespace Controller
{
    public class GameInstaller : MonoInstaller
    {
        public DataManager dataManagerPrefab;

        public override void InstallBindings() {
            // Container.Bind<DataManager>()
            //     .FromComponentInNewPrefab(dataManagerPrefab)
            //     .AsSingle();
            
            Container.Bind<DataManager>()
                .FromInstance(dataManagerPrefab)
                .AsSingle();
        }
    }
}