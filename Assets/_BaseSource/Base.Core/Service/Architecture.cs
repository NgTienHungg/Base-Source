using System;
using System.Collections.Generic;
using System.Linq;
using Base.Architecture;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.Core
{
    public abstract class Architecture : LiveSingleton<Architecture>, IArchitecture
    {
        [ShowInInspector]
        protected Dictionary<Type, IService> serviceDictionary = new Dictionary<Type, IService>();

        private bool isInitialized;

        protected override void OnAwake() { }

        protected abstract void Inject();
        
        protected void GetAllServices(IArchitecture architecture)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var services = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsClass && typeof(IService).IsAssignableFrom(t))
                .ToArray();

            // foreach (var service in services)
            // {
            //     ServiceTypes.Add(service);
            // }
        }

        public bool HasService<TService>() where TService : IService {
            return serviceDictionary.ContainsKey(typeof(TService));
        }

        public TService GetService<TService>() where TService : IService {
            var type = typeof(TService);
            if (serviceDictionary.ContainsKey(type)) {
                return (TService)serviceDictionary[type];
            }

            Debug.LogWarning($"Service {type} not found in this architect");
            return default;
        }

        public void RegisterService<TService>(TService service) where TService : IService {
            var type = service.GetType();
            if (!serviceDictionary.ContainsKey(type)) {
                serviceDictionary.Add(type, service);
                service.OnInit();
            }
            else {
                Debug.LogWarning($"Service {type} already exist in this architect");
            }
        }

        public void UnregisterService<TService>() where TService : IService {
            var type = typeof(TService);
            if (!serviceDictionary.ContainsKey(type)) {
                var serviceInstance = Activator.CreateInstance(type) as IService;
                serviceDictionary.Add(type, serviceInstance);
                serviceInstance.OnInit();
            }
            else {
                Debug.LogWarning($"Service {type} already exist in this architect");
            }
        }

        public void StartServices() {
            Debug.Log($"[ARCHITECT] Start all services...");
            foreach (var service in serviceDictionary.Values) {
                service.OnStart();
            }
        }

        public void DisposeServices() {
            Debug.Log($"[ARCHITECT] Dispose all services...");
            foreach (var service in serviceDictionary.Values) {
                service.OnDispose();
            }
        }

        private void Update() {
            if (!isInitialized)
                return;

            foreach (var service in serviceDictionary.Values) {
                service.OnUpdate();
            }
        }
    }
}