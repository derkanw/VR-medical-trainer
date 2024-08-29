using BNG;
using Orbox.Localization;
using Orbox.Signals;
using Orbox.Utils;

namespace MerckPreprodVR
{
    public interface IComposition
    {
        void Destroy();
        ITimers GetTimers();
        IGame GetGame();
        IUIRoot GetUIRoot();
        EnumCache GetEnumCache(); 
        IViewFactory GetViewFactory();
        ISoundManager GetSoundManager();
        IConfiguration GetConfiguration();
        IResourceManager GetResourceManager();
        IEventPublisher GetMonoEventsProvider();
        IPlatformFactory GetPlatformFactory();
        IDeviceInfo GetDeviceInfo();
        IAppSettings GetAppSettings();
        IExperience GetExperience();
        IStorage GetStorage();
        ILogger GetLogger();
        IVRCameraRig GetCameraRig();
        IUserInput GetUserInput();
        InputBridge GetInputBridge();

    }
    
}
