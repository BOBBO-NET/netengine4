using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BobboNet.ConsoleCommands;

namespace BobboNet
{
    public class DevConsoleLogic : IInitializable
    {
        private const string messageEmptyCommand = "[DEV] Unrecognized command.";
        private List<DevConsoleCommandBase> commands = new List<DevConsoleCommandBase>();

        //
        //  Init
        //

        public DevConsoleLogic(List<DevConsoleCommandBase> commands)
        {
            this.commands = commands;
        }

        public void Initialize()
        {
            foreach (var command in commands)
            {
                command.Initialize(this);
            }
        }

        //
        //  Public Methods
        //

        public IEnumerable<DevConsoleCommandBase> GetCommands() => commands;

        public string ProcessCommand(string rawCommand)
        {
            // If this command is nothing, EXIT EARLY
            if (string.IsNullOrWhiteSpace(rawCommand)) return messageEmptyCommand;

            // If we couldn't parse this command for some reason, EXIT EARLY
            string[] args = ParseString(rawCommand);
            if (args == null || args.Length == 0) return messageEmptyCommand;

            // Find the command to process. If there isn't one, EXIT EARLY.
            DevConsoleCommandBase command = FindCommandByName(args[0]);
            if (command == null) return messageEmptyCommand;

            // OTHERWISE - we found a valid command. Process it!
            return command.ExecuteCommand(args);
        }

        //
        //  Private Methods
        //

        private string[] ParseString(string rawCommand)
        {
            string[] rawArgs = rawCommand.Split(' ');
            List<string> cleanArgs = new List<string>();

            for (int i = 0; i < rawArgs.Length; i++)
            {
                if (rawArgs[i] != "") cleanArgs.Add(rawArgs[i]);
            }

            return cleanArgs.ToArray();
        }

        private DevConsoleCommandBase FindCommandByName(string commandName)
        {
            foreach (DevConsoleCommandBase command in commands)
            {
                if (command.CommandName == commandName) return command;
            }

            return null;
        }
    }
}