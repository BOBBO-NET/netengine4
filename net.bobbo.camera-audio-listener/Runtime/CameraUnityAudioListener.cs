using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet
{
    public class CameraUnityAudioListener : ICameraAudioListener
    {
        public void Setup(Camera mainCamera)
        {
            mainCamera.gameObject.AddComponent<AudioListener>();
        }
    }

}
