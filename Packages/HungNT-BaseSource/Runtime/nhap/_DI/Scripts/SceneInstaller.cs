using Zenject;

namespace _DI.Scripts
{
    public class SceneInstaller : MonoInstaller
    {
        // public SoundManager soundManager;

        public override void InstallBindings() {
            // Container.Bind<SoundManager>().FromInstance(soundManager).AsSingle();
            Container.Bind<ILogger>().To<Logger>().AsSingle();
        }
    }
}