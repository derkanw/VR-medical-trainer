using BNG;
using Orbox.Utils;

namespace MerckPreprodVR
{
    public class PCPlatformFactory : IPlatformFactory
    {
        public IUIRoot CreateUIRoot(IResourceManager resourceManager)
        {
            var uiRoot = resourceManager.CreatePrefabInstance<EComponents, IUIRoot>(EComponents.UIRoot);
            return uiRoot;
        }

        public VRUISystem CreateVRUISystem(IResourceManager resourceManager)
        {
            var imodule = resourceManager.CreatePrefabInstance<EComponents, VRUISystem>(EComponents.EventSystem);
            return imodule;
        }

        public VRCameraRig CreateCameraRig(IResourceManager resourceManager)
        {
            return resourceManager.CreatePrefabInstance<EComponents, VRCameraRig>(EComponents.CameraRig);
        }

        public IDeviceInfo GetDeviceInfo()
        {
            return new PCDeviceInfo();
        }

        public IAppSettings GetAppSettings()
        {
            return new PCAppSettings();
        }

        public IViewFactory CreateViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
        {
            // Here we create ViewFactory specific for PC, it has to be like 'PCViewFactory' ))
            var viewFactory = new ViewFactory(uiRoot, resourceManager);

            return viewFactory;
        }
    }
}