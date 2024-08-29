using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class OnboardingPhase : IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;
        private IWorldSpaceModel _welcome;
        private IWorldSpaceView _welcomeView;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;

        private bool _restartPhase = false;

        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };

        public OnboardingPhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
        {
            _headTrasform = headTransform;
            _resourceManager = rm;
            _viewFactory = viewFactory;
            _soundManager = soundManager;
            _timers = timers;
            _tvView = tvView;
        }

        private void Initialize()
        {
            if (_restartPhase)
            {
                //do nothing
            }
            else
            {
                var _worldSpaceRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, WorldSpaceRoot>(EPhaseRoots.WorldSpaceRoot);
                _welcomeView = _viewFactory.CreateWelcomeView();
                _welcome = new WorldSpaceModel(_welcomeView, _headTrasform, _worldSpaceRoot.GetPlaceholder());
            }
        }

        public IPromise Start()
        {
            Initialize();
            float timeOffset = 1f;
            return _timers.Wait(0f)
                .Done(() => _tvView.PlayLogo())
                .Done(() => _soundManager.Play(EAudio.Task_1_0))
                .Then(_welcome.Show)
                .Then(WaitForStart)
                .Then(_welcome.Hide)
                .Then(_tvView.PlayIntro)
                .Then(() => _timers.Wait(timeOffset))
                .Then(Stop);
        }

        public IPromise Stop()
        {
            _restartPhase = true;
            PhaseFinished();
            return new Deferred().Resolve();
        }
        
        private IPromise WaitForStart()
        {
            var deferred = new Deferred();
            _welcome.ButtonClicked += StartButton;

            void StartButton()
            {
                deferred.Resolve();
                _welcome.ButtonClicked -= StartButton;
            }

            return deferred;
        }

        public void Update()
        {
            
        }
    }
}