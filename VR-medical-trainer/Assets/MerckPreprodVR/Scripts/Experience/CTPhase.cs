using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class CTPhase : IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;
        private Tooltip _ctTooltip;
        private LungsScanner _lungsScanner;
        private CTRoot _ctRoot;

        private bool _restartPhase = false;

        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };
        public event Action ScanningCompleted = () => { };

        public CTPhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
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
                _ctTooltip.Reactivate();
            }
            else
            {
                _ctRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, CTRoot>(EPhaseRoots.CTRoot);
                var ctTooltipView = _viewFactory.CreateCtTooltipView();
                _ctTooltip = new Tooltip(ctTooltipView, _headTrasform, _ctRoot.CtTooltip());
            }

            _lungsScanner = _resourceManager.CreatePrefabInstance<EGrabbables, LungsScanner>(EGrabbables.LungsScanner);
            _lungsScanner.SetPosition(_ctRoot.LungsScanner());
            _lungsScanner.gameObject.SetActive(false);
        }

        public void Update()
        {
            
        }

        public IPromise Start()
        {
            Initialize();
            return _tvView.PlayCTPhaseStart()
                .Done(() => _soundManager.Play(EAudio.Task_4_1))
                .Done(() => _ctTooltip.Show())
                .Done(() =>
                {
                    _lungsScanner.gameObject.SetActive(true);
                    _lungsScanner.ScannerUpdated += UpdateScreen;
                    _lungsScanner.Highlight();
                    _tvView.ShowImage(0);
                })
                .Then(WaitForComplete)
                .Then(_ctTooltip.Complete)
                .Then(Stop);
        }

        public IPromise Stop()
        {
            _lungsScanner.gameObject.SetActive(false);
            _lungsScanner = null;

            _restartPhase = true;

            return _tvView.PlayCTPhaseEnd()
                .Done(PhaseFinished);
        }

        public void UpdateScreen()
        {
            float num = _lungsScanner.GetSliderPercent();
            _tvView.ShowImage(num);
            if (num == 1f)
            {
                ScanningCompleted();
            }
        }
        
        private IPromise WaitForComplete()
        {
            var deferred = new Deferred();

            ScanningCompleted += CompleteScanning;

            void CompleteScanning()
            {
                ScanningCompleted -= CompleteScanning;

                deferred.Resolve();
            }

            return deferred;
        }
    }
}