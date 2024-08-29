using Orbox.Utils;

namespace MerckPreprodVR
{
    public class OculusQuestPlatformFactory : OculusPlatformFactory
    {
        public override IAppSettings GetAppSettings()
        {
            return new OculusQuestAppSettings();
        }
    }
}