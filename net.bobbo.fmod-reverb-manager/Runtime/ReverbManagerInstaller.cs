using UnityEngine;
using Zenject;

namespace BobboNet.Audio
{
    [CreateAssetMenu(fileName = "ReverbManagerInstaller", menuName = "Installers/ReverbManagerInstaller")]
    public class ReverbManagerInstaller : ScriptableObjectInstaller<ReverbManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInitializable>().To<ReverbManager>().AsSingle();
        }
    }
}