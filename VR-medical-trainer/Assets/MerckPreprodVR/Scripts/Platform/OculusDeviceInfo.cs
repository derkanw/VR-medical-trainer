using UnityEngine;

namespace MerckPreprodVR
{
    public class OculusDeviceInfo : IDeviceInfo
    {
        public string Id => SystemInfo.deviceUniqueIdentifier;

        public string Type => OVRPlugin.GetSystemHeadsetType().ToString();

        public string Name => OVRPlugin.productName;
    }
}