using UnityEngine;
using Zenject;

namespace BobboNet
{
    [CreateAssetMenu(fileName = "CameraUnityAudioListenerInstaller", menuName = "Installers/CameraUnityAudioListenerInstaller")]
    public class CameraUnityAudioListenerInstaller : ScriptableObjectInstaller<CameraUnityAudioListenerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraAudioListener>().To<CameraUnityAudioListener>().AsSingle();
        }
    }
}
