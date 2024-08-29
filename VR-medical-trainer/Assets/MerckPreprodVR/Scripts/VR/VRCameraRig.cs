using UnityEngine;

namespace MerckPreprodVR
{
    public class VRCameraRig : MonoBehaviour, IVRCameraRig
    {
        public Transform Player;
        public Transform PlayerTransform => Player;
    }
}