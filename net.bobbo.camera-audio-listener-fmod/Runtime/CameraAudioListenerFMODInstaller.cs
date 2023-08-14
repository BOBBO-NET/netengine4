using UnityEngine;
using Zenject;

namespace BobboNet
{
    [CreateAssetMenu(fileName = "CameraAudioListenerFMODInstaller", menuName = "Installers/CameraAudioListenerFMODInstaller")]
    public class CameraAudioListenerFMODInstaller : ScriptableObjectInstaller<CameraAudioListenerFMODInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraAudioListener>().To<CameraAudioListenerFMOD>().AsSingle();
        }
    }
}
