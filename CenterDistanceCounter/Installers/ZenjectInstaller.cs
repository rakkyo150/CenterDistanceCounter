using Zenject;

namespace CenterDistanceCounter.Installers
{
    class ZenjectInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<BeatmapObjectManager>()
                .To<BeatmapObjectManager>()
                .AsCached();
        }
    }
}
