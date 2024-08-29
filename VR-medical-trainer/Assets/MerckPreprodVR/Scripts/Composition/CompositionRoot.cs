using System.Text.RegularExpressions;
using BNG;
using Orbox.Localization;
using Orbox.Signals;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class CompositionRoot : MonoBehaviour
    {
        private static readonly IComposition Composition = new Composition();
        private void OnDestroy()
        {
            Composition.Destroy();
        }

        public static ITimers GetTimers()
        {
            return Composition.GetTimers();
        }
        
        public static IGame GetGame()
        {
            return Composition.GetGame();
        }

        public static IUIRoot GetUIRoot()
        {
            return Composition.GetUIRoot();
        }

        public static EnumCache GetEnumCache()
        {
            return Composition.GetEnumCache();
        }

        public static IViewFactory GetViewFactory()
        {
            return Composition.GetViewFactory();
        }

        public static ISoundManager GetSoundManager()
        {
            return Composition.GetSoundManager();
        }

        public static IConfiguration GetConfiguration()
        {
            return Composition.GetConfiguration();
        }

        public static IResourceManager GetResourceManager()
        {
            return Composition.GetResourceManager();
        }

        public static IEventPublisher GetMonoEventsProvider()
        {
            return Composition.GetMonoEventsProvider();
        }

        public static IDeviceInfo GetDeviceInfo()
        {
            return Composition.GetDeviceInfo();
        }

        public static IAppSettings GetAppSettings()
        {
            return Composition.GetAppSettings();
        }

        public static IExperience GetExperience()
        {
            return Composition.GetExperience();
        }

        public static IStorage GetStorage()
        {
            return Composition.GetStorage();
        }

        public static IVRCameraRig GetCameraRig()
        {
            return Composition.GetCameraRig();
        }

        public static IUserInput GetUserInput()
        {
            return Composition.GetUserInput();
        }
        
        public static InputBridge GetInputBridge()
        {
            return Composition.GetInputBridge();
        }

        public static ILogger GetLogger()
        {
            return Composition.GetLogger();
        }

        /*public static ITVView GetTVView()
        {
            return Composition.GetTVView();
        }*/
    }
}