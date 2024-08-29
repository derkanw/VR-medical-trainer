using System;
using Orbox.Async;
using Orbox.Signals;
using Orbox.Utils;
using UnityEngine;

namespace MerckPreprodVR
{
    public class Experience : IExperience, IUpdatable
    {
        private IPhase _onboardingPhase;
        private IPhase _medicalHistoryPhase;
        private IPhase _temperaturePhase;
        private IPhase _saturationPhase;
        private IPhase _ctPhase;
        private IPhase _summaryPhase;

        private IPhase _currentPhase;

        private ITVView _tVView;
        
        private IEventPublisher _eventPublisher;
        
        public event Action<float> ProgressUpdated;
        public event Action ExperienceFinished = () => { };
        
        public Experience (IEventPublisher publisher, Transform headTransform,
            IViewFactory viewFactory, IUserInput userInput, IResourceManager resourceManager, ISoundManager soundManager, ITimers timers)
        {
            _eventPublisher = publisher;

            _eventPublisher.Add(this);

            _tVView = viewFactory.CreateTVView();

            _onboardingPhase = new OnboardingPhase(headTransform, resourceManager,  soundManager, viewFactory, timers, _tVView);
            _medicalHistoryPhase = new MedicalHistoryPhase(headTransform, resourceManager, soundManager, viewFactory, timers, _tVView );
            _temperaturePhase = new TemperaturePhase(headTransform, resourceManager, soundManager, viewFactory, timers, _tVView);
            _saturationPhase = new SaturationPhase(headTransform, resourceManager, soundManager, viewFactory, timers, _tVView);
            _ctPhase = new CTPhase(headTransform, resourceManager, soundManager, viewFactory, timers, _tVView);
            _summaryPhase = new SummaryPhase(headTransform, resourceManager, soundManager, viewFactory, timers, _tVView);

            _onboardingPhase.PhaseFinished += () => StartPhase(_medicalHistoryPhase);
            _medicalHistoryPhase.PhaseFinished += () => StartPhase(_temperaturePhase);
            _temperaturePhase.PhaseFinished += () => StartPhase(_saturationPhase);
            _saturationPhase.PhaseFinished += () => StartPhase(_ctPhase);
            _ctPhase.PhaseFinished += () => StartPhase(_summaryPhase);
            _summaryPhase.PhaseFinished += () => Stop();
        }

        private IPromise StartPhase(IPhase newPhase)
        {
            _currentPhase = newPhase;
            return _currentPhase.Start();
        }

        public IPromise Start()
        {
            return StartPhase(_onboardingPhase);
        }

        public IPromise Stop()
        {
            ExperienceFinished();
            return new Deferred().Resolve();
        }

        public void Restart()
        {
            Start();
        }
        
        public void Update()
        {
            _currentPhase.Update();
        }
    }
}