using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;
using DG.Tweening;

namespace MerckPreprodVR
{
    public enum TemperatureState
    {
        OutsideTargetZone,
        OutsideTargetZoneReady, // device in hand
        InTargetZoneProcess,
        InTargetZoneDone,
        OutsideTargetZoneDone
    }

    public class TemperaturePhase : IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;
        private Collider _thermometerCollider;
        private Animator _thermometerAnimator;
        private string _oldTag;
        private bool _thermometerGrabbed = true;
        //
        private TemperatureRoot _temperatureRoot;
        private Thermometer _thermometer;
        private ThermometerSnapZone2 _thermometerSnapZone;
        private Tooltip _thermometerTooltip;
        private Tooltip _thermometerSnapZoneTooltip;

        private bool _restartPhase = false;
        private TemperatureState state = TemperatureState.OutsideTargetZone;

        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };
        
        public TemperaturePhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
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
                _thermometerTooltip.Reactivate();
                _thermometerSnapZoneTooltip.Reactivate();
            }
            else
            {
                _temperatureRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, TemperatureRoot>(EPhaseRoots.TemperatureRoot);
                var _thermometerTooltipView = _viewFactory.CreateThermometerTooltipView();
                _thermometerTooltip = new Tooltip(_thermometerTooltipView, _headTrasform, _temperatureRoot.ThermometerTooltip());
                var _thermometerSnapZoneTooltipView = _viewFactory.CreateThermometerSnapZoneTooltipView();
                _thermometerSnapZoneTooltip = new Tooltip(_thermometerSnapZoneTooltipView, _headTrasform,
                    _temperatureRoot.ThermometerSnapZoneTooltip());
            }

                state = TemperatureState.OutsideTargetZone;

                _thermometer = _resourceManager.CreatePrefabInstance<EGrabbables, Thermometer>(EGrabbables.Thermometer);
                _thermometer.SetPosition(_temperatureRoot.Thermometer());

                _thermometerSnapZone = _resourceManager.CreatePrefabInstance<ESnapZones, ThermometerSnapZone2>(ESnapZones.ThermometerSnapZone);
                _thermometerSnapZone.SetPosition(_temperatureRoot.ThermometerSnapZone());

                _thermometerAnimator = _thermometer.GetComponentInChildren<Animator>();
                _thermometerCollider = _thermometer.GetComponentInChildren<Collider>();
                _oldTag = _thermometerCollider.gameObject.tag;
                _thermometerCollider.tag = "TargetItem";
        }

        public void Update()
        {
            _thermometerTooltip.Update();
            _thermometerSnapZoneTooltip.Update();
        }

        public IPromise Start()
        {
            Initialize();

            return _tvView.PlayTemperaturePhaseStart()
                .Done(() => _soundManager.Play(EAudio.Task_2_0))
                .Then(() => _timers.Wait(1f))
                .Done(_thermometer.Highlight)
                .Then(_thermometerTooltip.Show)
                .Done(() =>
                {
                    _thermometerGrabbed = false;
                    _thermometerSnapZone.InSnapZone += () => ThermometerEnterSnapZone();
                    _thermometerSnapZone.OutSnapZone += () => ThermometerExitSnapZone();
                    _thermometer.Grabbed += () => ThermometerGrab();
                    _thermometer.Dropped += () => ThermometerDrop();
                });
        }

        public IPromise Stop()
        {
            _thermometer.ClearEvents();
            _thermometerSnapZone.ClearEvents();
            _thermometerSnapZone.Disable();
            _thermometerSnapZoneTooltip.Complete();
            _thermometerCollider.gameObject.tag = _oldTag;
            _thermometer.Hide();
            _thermometer = null;
            _thermometerSnapZone = null;

            _restartPhase = true;

            return _tvView.PlayClip(5).Done(() => PhaseFinished());
        }

        public IPromise ThermometerGrab()
        {
            if (state == TemperatureState.OutsideTargetZone)
            {
                state = TemperatureState.OutsideTargetZoneReady;
                DOTween.KillAll(true);
                if (!_thermometerGrabbed)
                {
                    _soundManager.PlayAndNotify(EAudio.Task_2_1);
                    _thermometerGrabbed = true;
                    _thermometerTooltip.Complete();
                    _thermometerSnapZoneTooltip.Show();
                }
                _thermometer.Unhighlight();
                _thermometerSnapZone.Show();
                return new Deferred().Resolve();
            }
            return new Deferred().Resolve();
        }

        public IPromise ThermometerDrop()
        {
            if ((state == TemperatureState.InTargetZoneProcess) || (state == TemperatureState.OutsideTargetZoneReady))
            {
                state = TemperatureState.OutsideTargetZone;
                _thermometerSnapZone.SwitchModels(false);
                DOTween.KillAll(true);
                _thermometer.Highlight();
                return new Deferred().Resolve();
            }
            if ((state == TemperatureState.InTargetZoneDone) || (state == TemperatureState.OutsideTargetZoneDone))
            {
                Stop(); // end of phase
            }
            return new Deferred().Resolve();
        }

        public IPromise ThermometerEnterSnapZone()
        {
            if (state == TemperatureState.OutsideTargetZoneReady)
            {
                state = TemperatureState.InTargetZoneProcess;
                _thermometerSnapZone.SwitchModels(true); // ok

                DOTween.KillAll(true);
                _soundManager.PlayAndNotify(EAudio.Pip_start);
                _thermometerAnimator.SetTrigger("Measure");
                DOTween.Sequence().AppendInterval(4.083f).OnStepComplete(() =>
                {
                    _soundManager.PlayAndNotify(EAudio.Pip_end);
                    _thermometerAnimator.SetTrigger("StopMeasure");
                    state = TemperatureState.InTargetZoneDone;
                });
            }
            return new Deferred().Resolve();
        }

        public IPromise ThermometerExitSnapZone()
        {
            // Exited before completion
            if (state == TemperatureState.InTargetZoneProcess)
            {
                state = TemperatureState.OutsideTargetZoneReady;
                DOTween.KillAll(true);
                _thermometerSnapZone.SwitchModels(false);
                _soundManager.PlayAndNotify(EAudio.Pip_interrupted);
                _thermometerAnimator.SetTrigger("StopMeasure");
            }

            // Completed and Exited
            if (state == TemperatureState.InTargetZoneDone)
            {
                state = TemperatureState.OutsideTargetZoneDone;
                DOTween.KillAll(true);

                _thermometerSnapZone.ClearEvents();
                _thermometerSnapZone.Disable();
            }
            return new Deferred().Resolve();
        }
    }
}