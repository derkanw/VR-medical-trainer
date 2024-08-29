using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class MedicalHistoryPhase :  IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;

        private IWorldSpaceModel _medicalHistory;
        private IWorldSpaceView _medicalHistoryView;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;

        private bool _restartPhase = false;

        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };

        public MedicalHistoryPhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
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
                // do nothing
            }
            else
            {
                var worldSpaceRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, WorldSpaceRoot>(EPhaseRoots.WorldSpaceRoot);
                _medicalHistoryView = _viewFactory.CreateMedicalHistoryView();
                _medicalHistory = new WorldSpaceModel(_medicalHistoryView, _headTrasform, worldSpaceRoot.GetPlaceholder());
            }
        }
        public void Update()
        {
        }

        public IPromise Start()
        {
            Initialize();
            float timeOffset = 1f;

            return _tvView.PlayMedicalHistoryPhaseStart()
                .Done(() => _soundManager.Play(EAudio.Task_1_3))
                .Then(_medicalHistory.Show)
                .Then(WaitForInvite)
                .Then(_medicalHistory.Hide)
                .Then(_tvView.PlayMedicalHistoryPhaseEnd)
                .Then(() => _timers.Wait(timeOffset))
                .Done(() => _resourceManager.CreatePrefabInstance(ECharacter.Carla))
                .Then(() => _timers.Wait(timeOffset))
                .Then(Stop);
        }

        public IPromise Stop()
        {
            _restartPhase = true;
            PhaseFinished();
            return new Deferred().Resolve();
        }

        private IPromise WaitForInvite()
        {
            var deferred = new Deferred();

            _medicalHistory.ButtonClicked += InviteButton;

            void InviteButton()
            {
                //Debug.Log("medical. 'Invite' cliked");
                _medicalHistory.ButtonClicked -= InviteButton;
                deferred.Resolve();
            }

            return deferred;
        }
    }
}