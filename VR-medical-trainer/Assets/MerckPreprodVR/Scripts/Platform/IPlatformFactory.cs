using BNG;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface IPlatformFactory
    {
        IUIRoot CreateUIRoot(IResourceManager resourceManager);
        VRUISystem CreateVRUISystem(IResourceManager resourceManager);
        VRCameraRig CreateCameraRig(IResourceManager resourceManager);
        IViewFactory CreateViewFactory(IUIRoot uiRoot, IResourceManager resourceManager);
        IDeviceInfo GetDeviceInfo();
        IAppSettings GetAppSettings();
    }
}