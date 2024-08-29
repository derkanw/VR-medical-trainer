using UnityEngine;

namespace MerckPreprodVR
{
    public class PCDeviceInfo : IDeviceInfo
    {
        public string Id => SystemInfo.deviceUniqueIdentifier;

        public string Type => SystemInfo.deviceType.ToString("G");

        public string Name => SystemInfo.deviceName;
    }
}