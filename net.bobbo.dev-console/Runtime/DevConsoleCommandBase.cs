using UnityEngine;
using Zenject;

namespace BobboNet.ConsoleCommands
{
    public abstract class DevConsoleCommandBase
    {
        public virtual void Initialize(DevConsoleLogic consoleLogic) { }

        public abstract string CommandName { get; }
        public abstract string ExecuteCommand(string[] args);
    }
}

