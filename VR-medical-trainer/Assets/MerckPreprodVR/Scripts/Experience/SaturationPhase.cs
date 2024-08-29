using System;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;
using DG.Tweening;

namespace MerckPreprodVR
{
    public enum SaturationState
    {
        OutsideTargetZoneReady,
        InTargetZoneReady,
        InTargetZoneProcess,
        InTargetZoneDone,
        OutsideTargetZoneDone
    }

    public class SaturationPhase : IPhase
    {
        private Transform _headTrasform;
        private IResourceManager _resourceManager;
        private IViewFactory _viewFactory;
        private ISoundManager _soundManager;
        private ITimers _timers;
        private ITVView _tvView;
        private string _oldTag;
        private Collider _pulseoximeterCollider;
        private bool _pulseoximeterGrabbed = false;
        private Animator _pulseoximeterAnimator;

        private SaturationRoot _saturationRoot;
        private Pulseoximeter _pulseoximeter;
        private PulseoximeterSnapZone _pulseoximeterSnapZone;
        private Tooltip _pulseoximeterTooltip;
        private Tooltip _pulseoximeterSnapZoneTooltip;
        private Tooltip _taskCompletedTooltip;

        private bool _restartPhase = false;
        private SaturationState state = SaturationState.OutsideTargetZoneReady;
        //
        public event Action PhaseFinished = () => { };
        public event Action PhaseStarted = () => { };

        public SaturationPhase(Transform headTransform, IResourceManager rm, ISoundManager soundManager, IViewFactory viewFactory, ITimers timers, ITVView tvView)
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
                _pulseoximeterTooltip.Reactivate();
                _pulseoximeterSnapZoneTooltip.Reactivate();
            }
            else
            {
                _saturationRoot = _resourceManager.CreatePrefabInstance<EPhaseRoots, SaturationRoot>(EPhaseRoots.SaturationRoot);
                var pulseoximeterTooltipView = _viewFactory.CreatePulseoximeterTooltipView();
                _pulseoximeterTooltip = new Tooltip(pulseoximeterTooltipView, _headTrasform, _saturationRoot.SaturationTooltip());
                var pulseoximeterSnapZoneTooltipView = _viewFactory.CreatePulseoximeterSnapZoneTooltipView();
                _pulseoximeterSnapZoneTooltip = new Tooltip(pulseoximeterSnapZoneTooltipView, _headTrasform,
                    _saturationRoot.PulseoximeterSnapZoneTooltip());
            }

            state = SaturationState.OutsideTargetZoneReady;

            _pulseoximeterSnapZone =_resourceManager.CreatePrefabInstance<ESnapZones, PulseoximeterSnapZone>(ESnapZones.PulseoximeterSnapZone);
            _pulseoximeterSnapZone.SetPosition(_saturationRoot.PulseoximeterSnapZone());

            _pulseoximeter = _resourceManager.CreatePrefabInstance<EGrabbables, Pulseoximeter>(EGrabbables.Pulseoximeter);
            _pulseoximeter.SetPosition(_saturationRoot.Pulseoximeter());

            _pulseoximeterAnimator = _pulseoximeter.GetComponentInChildren<Animator>();
            _pulseoximeterCollider = _pulseoximeter.GetComponentInChildren<Collider>();
            _oldTag = _pulseoximeterCollider.gameObject.tag;
            _pulseoximeterCollider.tag = "TargetItem";
        }

        public void Update()
        {
            _pulseoximeterTooltip.Update();
            _pulseoximeterSnapZoneTooltip.Update();
        }

        public IPromise Start()
        {
            Initialize();

            return _tvView.PlayClip(6)
                .Done(() => _soundManager.PlayAndNotify(EAudio.Task_3_0))
                .Then(() => _timers.Wait(1f))
                .Done(() => _pulseoximeterTooltip.Show())
                .Done(() => _pulseoximeter.Highlight())
                .Then(() =>
                {
                    _pulseoximeterGrabbed = false;
                    _pulseoximeterSnapZone.InSnapZone += () => PulseoximeterEnterSnapZone();
                    _pulseoximeterSnapZone.OutSnapZone += () => PulseoximeterExitSnapZone();
                    _pulseoximeter.Grabbed += () => PulseoximeterGrab();
                    _pulseoximeter.Dropped += () => PulseoximeterDrop();
                    return new Deferred().Resolve();
                });
        }

        public IPromise Stop()
        {
            _pulseoximeter.ClearEvents();
            _pulseoximeter.Hide();
            _pulseoximeterSnapZone.ClearEvents();
            _pulseoximeterSnapZone.Disable();
            _pulseoximeterSnapZoneTooltip.Complete();
            _pulseoximeterCollider.gameObject.tag = _oldTag;
            _pulseoximeter = null;
            _pulseoximeterSnapZone = null;

            _restartPhase = true;

            return _tvView.PlayClip(7).Done(PhaseFinished);
        }

        public IPromise PulseoximeterGrab()
        {
            // cannot grab device in process of measurement
            if (state == SaturationState.InTargetZoneProcess)
            {
                return new Deferred().Resolve();
            }

            DOTween.KillAll(true);
            return new Deferred().Resolve()
                .Then(() =>
                {
                    if (!_pulseoximeterGrabbed)
                    {
                        _soundManager.PlayAndNotify(EAudio.Task_3_1);
                        _pulseoximeterGrabbed = true;
                        _pulseoximeterTooltip.Complete();
                        _pulseoximeterSnapZoneTooltip.Show();
                    }
                    return new Deferred().Resolve();
                })
                .Done(() => _pulseoximeter.Unhighlight())
                .Done(() => _pulseoximeterSnapZone.Show());
        }

        public IPromise PulseoximeterDrop()
        {
            if (state == SaturationState.InTargetZoneReady)
            {
                state = SaturationState.InTargetZoneProcess;
                _pulseoximeter.SwitchZones(true);
                DOTween.KillAll(true);
                _pulseoximeterSnapZone.Hide();
                _pulseoximeterAnimator.SetTrigger("Measure");
                _soundManager.PlayAndNotify(EAudio.Pip_start);

                DOTween.Sequence().AppendInterval(6f).OnStepComplete(() =>
                {
                    _soundManager.PlayAndNotify(EAudio.Pip_end);
                    state = SaturationState.InTargetZoneDone;
                    _pulseoximeterSnapZoneTooltip.Complete();
                    Stop(); // on measurment completion
                });
                return new Deferred().Resolve();
            }

            DOTween.KillAll(true);
            _soundManager.PlayAndNotify(EAudio.Pip_interrupted);
            return new Deferred().Resolve()
                .Done(() => _pulseoximeter.Highlight())
                .Done(() => _pulseoximeterSnapZone.Hide());
        }

        public IPromise PulseoximeterEnterSnapZone()
        {
            if (state == SaturationState.OutsideTargetZoneReady)
            {
                state = SaturationState.InTargetZoneReady;
                _pulseoximeterSnapZone.SwitchModels(true);
            }
            return new Deferred().Resolve();
        }

        public IPromise PulseoximeterExitSnapZone()
        {
            if (state == SaturationState.InTargetZoneReady)
            {
                state = SaturationState.OutsideTargetZoneReady;
                _pulseoximeterSnapZone.SwitchModels(false);
            }
            //have to check Done
            if (state == SaturationState.InTargetZoneDone)
            {
                state = SaturationState.OutsideTargetZoneDone;
                _pulseoximeter.SwitchZones(false);

                //
                return Stop(); // works if you take off pulseoximeter after measurement
            }
            return new Deferred().Resolve();
        } 
    }
}