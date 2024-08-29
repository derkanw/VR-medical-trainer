using System;

namespace MerckPreprodVR
{
    public interface IApplicationEventsProvider
    {
        event Action Pause;
        event Action Resume;
        event Action LoseFocus;
        event Action GainFocus;
    }
}