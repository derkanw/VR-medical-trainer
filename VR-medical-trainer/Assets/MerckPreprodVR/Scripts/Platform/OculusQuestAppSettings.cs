using Unity.XR.Oculus;
using System.Linq;

namespace MerckPreprodVR
{
    public class OculusQuestAppSettings : IAppSettings
    {
        public void Setup()
        {
            Performance.TryGetAvailableDisplayRefreshRates(out var refreshRates);
            Performance.TrySetDisplayRefreshRate(refreshRates.Contains(90f) ? 90f : 72f);
            OVRPlugin.SetClientColorDesc(OVRPlugin.ColorSpace.Rift_CV1);
        }
    }
}