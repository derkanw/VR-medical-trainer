using System;
using BNG;
using Orbox.Localization;
using Orbox.Signals;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class Composition : IComposition
    {
        protected ITimers Timers;
        protected IGame Game;
        protected IUIRoot UIRoot;
        protected EnumCache EnumCache;
        protected IViewFactory ViewFactory;
        protected ISoundManager SoundManager;
        protected IConfiguration Configuration;
        protected IResourceManager ResourceManager;
        protected IEventPublisher MonoEventsProvider;
        protected IPlatformFactory PlatformFactory;
        protected IDeviceInfo DeviceInfo;
        protected IAppSettings AppSettings;
        protected IExperience Experience;
        protected IStorage Storage;
        protected ILogger Logger;
        protected IVRCameraRig CameraRig;
        protected InputBridge InputBridge;
        protected IUserInput UserInput;

        public void Destroy()
        {
            Timers = null;
            EnumCache = null;
            Configuration = null;

            Game = null;
            UIRoot = null;
            ViewFactory = null;
            MonoEventsProvider = null;

            AppSettings = null;
            SoundManager = null;
            ResourceManager.ClearPool();
            DeviceInfo = null;

            Experience = null;
            Storage = null;

            CameraRig = null;
            Logger = null;
            InputBridge = null;
            UserInput = null;
        }

        public ITimers GetTimers()
        {
            if (Timers == null)
            {
                Timers = MonoExtensions.MakeComponent<Timers>();
            }

            return Timers;
        }

        public IGame GetGame()
        {
            if (Game == null)
            {
                var enumCache = GetEnumCache();
                var storage = new Storage(enumCache);
                var configuration = GetConfiguration();
                var appEventsProvider = MonoExtensions.MakeComponent<ApplicationEventsProvider>();
                var appSettings = GetAppSettings();

                var game = new Game(appSettings, appEventsProvider, storage, configuration);
                Game = game;
            }

            return Game;
        }

        public IUIRoot GetUIRoot()
        {
            if (UIRoot == null)
            {
                var resourceManager = GetResourceManager();
                var platformFactory = GetPlatformFactory();
                UIRoot = platformFactory.CreateUIRoot(resourceManager);
            }

            return UIRoot;
        }

        public EnumCache GetEnumCache()
        {
            if (EnumCache == null)
            {
                EnumCache = new EnumCache();
            }

            return EnumCache;
        }

        public IViewFactory GetViewFactory()
        {
            if (ViewFactory == null)
            {
                var uiRoot = GetUIRoot();
                var resourceManager = GetResourceManager();
                var platformFactory = GetPlatformFactory();

                //ViewFactory = new ViewFactory(uiRoot, resourceManager);
                ViewFactory = platformFactory.CreateViewFactory(uiRoot, resourceManager);
            }
            return ViewFactory;
        }

        public ISoundManager GetSoundManager()
        {
            if (SoundManager == null)
            {
                var rm = GetResourceManager();
                var publisher = GetMonoEventsProvider();
                SoundManager = new SoundManager(rm, publisher);
            }

            return SoundManager;
        }

        public IConfiguration GetConfiguration()
        {
            if (Configuration == null)
            {
                Configuration = new ReleaseConfiguration();
            }
            return Configuration;
        }

        public IResourceManager GetResourceManager()
        {
            if (ResourceManager == null)
            {
                ResourceManager = new ResourceManager();
            }

            return ResourceManager;
        }

        public IEventPublisher GetMonoEventsProvider()
        {
            if (MonoEventsProvider == null)
            {
                MonoEventsProvider = MonoExtensions.MakeComponent<MBEventPublisher>();
            }

            return MonoEventsProvider;
        }

        public IPlatformFactory GetPlatformFactory()
        {
            if (PlatformFactory == null)
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        var headsetType = OVRPlugin.GetSystemHeadsetType();
                        if (headsetType == OVRPlugin.SystemHeadset.Oculus_Quest || headsetType == OVRPlugin.SystemHeadset.Oculus_Quest_2)
                            PlatformFactory = new OculusQuestPlatformFactory();
                        break;

                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.LinuxEditor:
                    case RuntimePlatform.WindowsEditor:
                        PlatformFactory = new PCPlatformFactory();
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }

            return PlatformFactory;
        }
        public ILogger GetLogger()
        {
            if (Logger == null)
            {
                var viewFactory = GetViewFactory();
                Logger = new AdvancedLogger(viewFactory);
            }

            return Logger;
        }
        public IDeviceInfo GetDeviceInfo()
        {
            if (DeviceInfo == null)
            {
                var factory = GetPlatformFactory();
                DeviceInfo = factory.GetDeviceInfo();
            }

            return DeviceInfo;
        }

        public IAppSettings GetAppSettings()
        {
            if (AppSettings == null)
            {
                var factory = GetPlatformFactory();
                AppSettings = factory.GetAppSettings();
            }

            return AppSettings;
        }

        public IExperience GetExperience()
        {
            if (Experience == null)
            {
                var userInput = GetUserInput();
                var rm = GetResourceManager();
                var cameraRig = GetCameraRig();
                var headTransform = cameraRig.PlayerTransform;
                var viewFactory = GetViewFactory();
                var mp = GetMonoEventsProvider();
                var sm = GetSoundManager();
                var timers = GetTimers();
                var experience = new Experience(mp, headTransform, viewFactory,userInput, rm, sm, timers);
                
                Experience = experience;
            }
            
            return Experience;
        }

        public IStorage GetStorage()
        {
            if (Storage == null)
            {
                var game = GetGame();
                Storage = game.GetStorage();
            }

            return Storage;
        }

        public IVRCameraRig GetCameraRig()
        {
            if (CameraRig == null)
            {
                var rm = GetResourceManager();
                var factory = GetPlatformFactory();

                CameraRig = factory.CreateCameraRig(rm);
            }

            return CameraRig;
        }

        public InputBridge GetInputBridge()
        {
            if (InputBridge == null)
            {
                var rm = GetResourceManager();
                InputBridge = rm.CreatePrefabInstance<EComponents, InputBridge>(EComponents.InputBridge);
            }

            return InputBridge;
        }
        public IUserInput GetUserInput()
        {
            if (UserInput == null)
            {
                var eventsProvider = GetMonoEventsProvider();
                var rm = GetResourceManager();
                var inputBridge = GetInputBridge();
                UserInput = new UserInput(eventsProvider, inputBridge);

            }

            return UserInput;
        }
    }
    
}