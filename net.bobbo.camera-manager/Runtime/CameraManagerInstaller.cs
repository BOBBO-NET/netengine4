using UnityEngine;
using Zenject;

namespace BobboNet
{
    [CreateAssetMenu(fileName = "CameraManagerInstaller", menuName = "Installers/CameraManagerInstaller")]
    public class CameraManagerInstaller : ScriptableObjectInstaller<CameraManagerInstaller>
    {
        public CameraManagerSettings settings;

        public override void InstallBindings()
        {
            Container.Bind<CameraManager>().FromNewComponentOnNewGameObject().WithGameObjectName("Camera Manager").AsSingle().NonLazy();
            Container.BindInstance(settings).AsSingle();
        }
    }
}
