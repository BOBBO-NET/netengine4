using System.Text;
using UnityEngine;
using Zenject;

namespace BobboNet.ConsoleCommands
{
    public class Help : DevConsoleCommandBase
    {
        public override string CommandName => "help";
        private DevConsoleLogic consoleLogic;

        public override void Initialize(DevConsoleLogic consoleLogic)
        {
            this.consoleLogic = consoleLogic;
        }

        public override string ExecuteCommand(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Command List:");
            foreach (DevConsoleCommandBase command in consoleLogic.GetCommands())
            {
                sb.AppendLine($"* '{command.CommandName}'");
            }

            return sb.ToString();
        }
    }
}

