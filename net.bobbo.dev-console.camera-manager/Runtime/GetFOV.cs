using System.Text;
using UnityEngine;
using Zenject;
using BobboNet;

namespace BobboNet.ConsoleCommands
{
    public class GetFOV : DevConsoleCommandBase
    {
        public override string CommandName => "get_fov";
        private CameraManager cameraManager;

        [Inject]
        public GetFOV(CameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
        }

        public override string ExecuteCommand(string[] args)
        {
            return $"FOV: {cameraManager.FOV}";
        }
    }
}

