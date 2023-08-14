using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace BobboNet
{
    public class CameraAudioListenerFMOD : ICameraAudioListener
    {
        public void Setup(Camera mainCamera)
        {
            mainCamera.gameObject.AddComponent<StudioListener>();
        }
    }

}
