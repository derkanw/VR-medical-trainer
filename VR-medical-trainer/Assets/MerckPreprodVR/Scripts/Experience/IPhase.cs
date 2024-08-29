using System;
using Orbox.Async;

namespace MerckPreprodVR
{
    public interface IPhase
    {
        IPromise Start();
        IPromise Stop();
        void Update();
        event Action PhaseFinished;
        event Action PhaseStarted;
    }
}