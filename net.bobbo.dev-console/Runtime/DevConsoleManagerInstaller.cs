using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BobboNet.ConsoleCommands;

namespace BobboNet
{
    [CreateAssetMenu(fileName = "DevConsoleManagerInstaller", menuName = "Installers/DevConsoleManagerInstaller")]
    public class DevConsoleManagerInstaller : ScriptableObjectInstaller<DevConsoleManagerInstaller>
    {
        public DevConsoleManager.Settings settings;

        public override void InstallBindings()
        {
            // Container.Bind<DevConsoleLogic>().AsSingle();
            Container.BindInterfacesAndSelfTo<DevConsoleLogic>().AsSingle();
            Container.Bind<DevConsoleManager>().FromComponentInNewPrefabResource("DevConsoleManagerPrefab").AsSingle().NonLazy();
            Container.BindInstance(settings).AsSingle();

            // Hook up console commands
            Container.Bind<DevConsoleCommandBase>().To<ConsoleCommands.Help>().AsSingle();
            Container.Bind<DevConsoleCommandBase>().To<ConsoleCommands.ListScenes>().AsSingle();
            Container.Bind<DevConsoleCommandBase>().To<ConsoleCommands.SetScene>().AsSingle();
        }
    }
}

