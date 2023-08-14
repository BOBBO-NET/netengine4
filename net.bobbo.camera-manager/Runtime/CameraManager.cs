using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BobboNet
{
    public class CameraManager : MonoBehaviour
    {
        public Camera MainCamera { private set; get; }
        public Camera EventCamera { private set; get; }
        public Camera OverlayCamera { private set; get; }

        public float FOV { get => MainCamera.fieldOfView; }

        private CameraManagerSettings settings;
        private ICameraAudioListener audioListener;

        //
        //  Initialization
        //

        [Inject]
        void Inject(CameraManagerSettings settings, ICameraAudioListener audioListener)
        {
            this.settings = settings;
            this.audioListener = audioListener;
        }

        void Awake()
        {
            MainCamera = new GameObject("Main Camera", typeof(Camera)).GetComponent<Camera>();
            OverlayCamera = new GameObject("Overlay Camera", typeof(Camera)).GetComponent<Camera>();
            EventCamera = new GameObject("Event Camera", typeof(Camera)).GetComponent<Camera>();

            MainCamera.transform.SetParent(transform);
            OverlayCamera.transform.SetParent(MainCamera.transform);
            EventCamera.transform.SetParent(MainCamera.transform);

            // Apply culling mask settings
            MainCamera.cullingMask = settings.mainCullingMask.value;
            OverlayCamera.cullingMask = settings.overlayCullingMask.value;

            // Configure the other cameras to not really render
            OverlayCamera.clearFlags = CameraClearFlags.Depth;
            EventCamera.cullingMask = 0;
            EventCamera.clearFlags = CameraClearFlags.Depth;

            audioListener.Setup(MainCamera);
        }

        //
        //  Tick
        //

        void Update()
        {
            EventCamera.fieldOfView = MainCamera.fieldOfView;
            OverlayCamera.fieldOfView = MainCamera.fieldOfView;
        }
    }
}
