using System.Text;
using UnityEngine;
using Zenject;

namespace BobboNet.ConsoleCommands
{
    public class SetScene : DevConsoleCommandBase
    {
        public override string CommandName => "set_scene";

        public override string ExecuteCommand(string[] args)
        {
            if (args.Length < 2)
            {
                return "Not enough arguments (type in the scene name after set_scene)";
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(args[1]);
            return "Changing scene to " + args[1];
        }
    }
}

