using System;
using UnityEngine;
using UnityEngine.XR;

namespace MerckPreprodVR
{
    public class Game : IGame
    {
        public event Action Started = () => {};
        public event Action Restarted = () => { };
        public event Action Paused = () => { };
        public event Action Resumed = () => { };

        private IStorage Storage;
        private IConfiguration Configuration;
        private IApplicationEventsProvider ApplicationEventsProvider;

        public Game(IAppSettings appSettings, IApplicationEventsProvider applicationEventsProvider, IStorage storage, IConfiguration configuration)
        {
            Storage = storage;
            Configuration = configuration;
            ApplicationEventsProvider = applicationEventsProvider;

            ApplicationEventsProvider.Pause += Pause;
            ApplicationEventsProvider.Resume += Resume;
            ApplicationEventsProvider.LoseFocus += OnFocusLost;
            ApplicationEventsProvider.GainFocus += OnFocusGained;

            appSettings.Setup();
            
        }

        private void OnFocusGained()
        {
            Resume();
            AudioListener.pause = false;
            //Time.timeScale = 1;
        }

        private void OnFocusLost()
        {
            Pause();
            AudioListener.pause = true;
           // Time.timeScale = 0;
        }

        public void Start()
        {
            Started();
        }

        public void Restart()
        {
            Restarted();
        }

        public void Pause()
        {
            Paused();
        }

        public void Resume()
        {
            Resumed();
        }

        public IStorage GetStorage()
        {
            return Storage;
        }
    }
}