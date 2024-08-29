using System;

namespace MerckPreprodVR
{
    public interface IGame
    {
        event Action Started;
        event Action Restarted;
        event Action Paused;
        event Action Resumed;

        void Start();
        void Restart();
        void Pause();
        void Resume();
        
        IStorage GetStorage();
    }
}