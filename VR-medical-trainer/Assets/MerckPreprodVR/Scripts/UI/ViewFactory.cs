using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class ViewFactory : IViewFactory
    {
        private IUIRoot UIRoot;
        private IResourceManager ResourceManager;

        public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
        {
            UIRoot = uiRoot;
            ResourceManager = resourceManager;
            UIRoot.WorldCanvas.gameObject.GetComponent<Canvas>().sortingOrder = 1;
        }

        public ILoggerView CreateLoggerView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ILoggerView>(EViews.LoggerView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreateMedicalHistoryTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.MedicalHistoryTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public IWorldSpaceView CreateMedicalHistoryView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, IWorldSpaceView>(EViews.MedicalHistoryView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreateThermometerTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.ThermometerTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }
        
        public ITooltipView CreateThermometerSnapZoneTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.ThermometerSnapZoneTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreatePulseoximeterTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.PulseoximeterTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreatePulseoximeterSnapZoneTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.PulseoximeterSnapZoneTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public IWorldSpaceView CreateWelcomeView()
		{
			var view = ResourceManager.CreatePrefabInstance<EViews, IWorldSpaceView>(EViews.WelcomeView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
		}

        public ITVView CreateTVView()
        {
            var view =  ResourceManager.CreatePrefabInstance<EViews, ITVView>(EViews.TVView);

            return view;
        }

        public ISummaryView CreateSummaryView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ISummaryView>(EViews.SummaryView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public IWorldSpaceView CreateRestartView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, IWorldSpaceView>(EViews.RestartView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreateCongratulationsTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.CongratulationsTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }

        public ITooltipView CreateCtTooltipView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, ITooltipView>(EViews.CtTooltipView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }
        
        public IResearchWindowView CreateResearchWindowView()
        {
            var view = ResourceManager.CreatePrefabInstance<EViews, IResearchWindowView>(EViews.ResearchWindowView);
            view.SetParent(UIRoot.WorldCanvas);

            return view;
        }
    }
}