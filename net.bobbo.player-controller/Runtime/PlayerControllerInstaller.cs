using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet.Player
{
    [CreateAssetMenu(fileName = "PlayerControllerInstaller", menuName = "Installers/PlayerControllerInstaller")]
    public class PlayerControllerInstaller : ScriptableObjectInstaller<PlayerControllerInstaller>
    {
        [SerializeField] List<ScriptableObjectInstallerBase> _behaviourInstallers = new List<ScriptableObjectInstallerBase>();
        public IReadOnlyList<ScriptableObjectInstallerBase> BehaviourInstallers => _behaviourInstallers;

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerController, PlayerController.Factory>().FromSubContainerResolve().ByNewGameObjectMethod(InstallPlayerControllerFacade).WithGameObjectName("PlayerController");
        }

        void InstallPlayerControllerFacade(DiContainer subContainer)
        {
            subContainer.Bind<PlayerController>().FromNewComponentOnRoot().AsSingle();

            foreach (var behaviourInstaller in _behaviourInstallers)
            {
                subContainer.Inject(behaviourInstaller);
                behaviourInstaller.InstallBindings();
            }
        }
    }
}

