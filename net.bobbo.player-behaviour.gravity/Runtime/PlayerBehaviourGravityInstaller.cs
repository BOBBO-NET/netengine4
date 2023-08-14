using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet.PlayerBehaviours
{
    [CreateAssetMenu(fileName = "PlayerBehaviourGravityInstaller", menuName = "Installers/PlayerBehaviourGravityInstaller")]
    public class PlayerBehaviourGravityInstaller : ScriptableObjectInstaller<PlayerBehaviourGravityInstaller>
    {
        public PlayerBehaviourGravity.Settings settings;

        public override void InstallBindings()
        {
            Container.BindInstance(settings).AsSingle();
            Container.Bind<PlayerBehaviour>().To<PlayerBehaviourGravity>().AsSingle();
        }
    }
}

