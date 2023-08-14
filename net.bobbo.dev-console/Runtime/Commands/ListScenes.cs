using System.Text;
using UnityEngine;
using Zenject;

namespace BobboNet.ConsoleCommands
{
    public class ListScenes : DevConsoleCommandBase
    {
        public override string CommandName => "list_scenes";
        private DevConsoleLogic consoleLogic;

        public override void Initialize(DevConsoleLogic consoleLogic)
        {
            this.consoleLogic = consoleLogic;
        }

        public override string ExecuteCommand(string[] args)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Scenes in build settings (there may be more or less...):");

            for (int i = 0; i < sceneCount; i++)
            {
                sb.AppendLine($"* '{UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(i).name}'");
            }

            return sb.ToString();
        }
    }
}

