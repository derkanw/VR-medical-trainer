using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class SummaryPhase : IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;
        private ISummaryModel _summary;
        private ISummaryView _summaryView;
        private ITooltip _congratulations;
        private IWorldSpaceView _restartView;
        private IWorldSpaceModel _restart;

        private bool _restartPhase = false;

        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };

        public SummaryPhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
        {
            _headTrasform = headTransform;
            _resourceManager = rm;
            _viewFactory = viewFactory;
            _soundManager = soundManager;
            _timers = timers;
            _tvView = tvView;
        }

        public IPromise Start()
        {
            Initialize();

            return _tvView.PlaySummaryPhaseStart()
                .Done(() => _soundManager.Play(EAudio.Task_5_1))
                .Done(() => MonoBehaviour.Destroy(MonoBehaviour.FindObjectOfType<Carla>().gameObject))
                .Then(_summary.Show)
                .Then(WaitForClicked)
                .Then(() => _timers.Wait(2f))
                .Then(_summary.Hide)
                .Then(_tvView.PlaySummaryPhaseEnd)
                .Done(() => _soundManager.Play(EAudio.Task_5_2))
                .Then(_congratulations.Show)
                .Then(() => _timers.Wait(4f))
                .Then(_congratulations.Hide)
                .Done(() => _soundManager.Play(EAudio.Task_5_3))
                .Then(_restart.Show)
                .Then(WaitForRestart)
                .Then(_restart.Hide)
                .Then(Stop);
        }

        public IPromise Stop()
        {
            _restartPhase = true;
            PhaseFinished();
            return new Deferred().Resolve();
        }

        public void Update()
        {
            _congratulations.Update();
        }

        private void Initialize()
        {
            if (_restartPhase)
            {
                // do nothing )
            }
            else
            {
                var worldSpaceRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, WorldSpaceRoot>(EPhaseRoots.WorldSpaceRoot);
                var summaryRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, SummaryRoot>(EPhaseRoots.SummaryRoot);

                _summaryView = _viewFactory.CreateSummaryView();
                _summary = new SummaryModel(_summaryView, _headTrasform, worldSpaceRoot.GetPlaceholder());

                var congratulationsView = _viewFactory.CreateCongratulationsTooltipView();
                _congratulations = new Tooltip(congratulationsView, _headTrasform, worldSpaceRoot.GetPlaceholder());

                _restartView = _viewFactory.CreateRestartView();
                _restart = new WorldSpaceModel(_restartView, _headTrasform, worldSpaceRoot.GetPlaceholder());
            }
        }

        private IPromise WaitForClicked()
        {
            var deferred = new Deferred();

            _summary.ContinueButtonClicked += ContinueButton;
            _summary.DismissButtonClicked += DismissButton;

            void ContinueButton()
            {
                _summary.DisableButton(ESummaryButton.DismissButton);
                _summary.ContinueButtonClicked -= ContinueButton;
                _summary.DismissButtonClicked -= DismissButton;
                deferred.Resolve();
            }

            void DismissButton()
            {
                _summary.DisableButton(ESummaryButton.ContinueButton);
                _summary.ContinueButtonClicked -= ContinueButton;
                _summary.DismissButtonClicked -= DismissButton;
                deferred.Resolve();
            }

            return deferred;
        }

        private IPromise WaitForRestart()
        {
            var deferred = new Deferred();

            _restart.ButtonClicked += RestartButton;

            void RestartButton()
            {
                _restart.ButtonClicked -= RestartButton;
                deferred.Resolve();
            }

            return deferred;
        }
    }
}