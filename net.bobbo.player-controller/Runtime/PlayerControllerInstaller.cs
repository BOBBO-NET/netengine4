using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet.Player
{
    [CreateAssetMenu(fileName = "PlayerControllerInstaller", menuName = "Installers/PlayerControllerInstaller")]
    public class PlayerControllerInstaller : ScriptableObjectInstaller<PlayerControllerInstaller>
    {
        public List<ScriptableObjectInstaller> behaviourInstallers = new List<ScriptableObjectInstaller>();

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerController, PlayerController.Factory>().FromSubContainerResolve().ByNewGameObjectMethod(InstallPlayerControllerFacade);
        }

        void InstallPlayerControllerFacade(DiContainer subContainer)
        {
            subContainer.Bind<PlayerController>().AsSingle();

            foreach (var behaviourInstaller in behaviourInstallers)
            {
                // behaviourInstaller.Install(subContainer);
            }
        }
    }
}

