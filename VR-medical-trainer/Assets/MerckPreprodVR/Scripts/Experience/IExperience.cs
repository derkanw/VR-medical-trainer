using System;
using Orbox.Async;

namespace MerckPreprodVR
{
    public interface IExperience
    {
        IPromise Start();
        IPromise Stop();
        void Restart();
        event Action<float> ProgressUpdated;
        event Action ExperienceFinished;
    }
}