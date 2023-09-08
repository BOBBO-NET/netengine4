using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BobboNet
{
    [System.Serializable]
    public class CameraManagerSettings
    {
        public LayerMask mainCullingMask = ~0;
        public LayerMask overlayCullingMask;
    }
}