using UnityEngine;

namespace MerckPreprodVR
{
    public class PCAppSettings : IAppSettings
    {
        public void Setup()
        {
            Application.targetFrameRate = 60;
        }
    }
}