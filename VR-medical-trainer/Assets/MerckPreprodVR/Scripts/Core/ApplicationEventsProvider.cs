using System;
using UnityEngine;

namespace MerckPreprodVR
{
    public class ApplicationEventsProvider: MonoBehaviour, IApplicationEventsProvider
    {
        public event Action Pause = () => { };
        public event Action Resume = () => { };
        public event Action LoseFocus = () => { };
        public event Action GainFocus = () => { };

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                GainFocus();
            }
            else
            {
                LoseFocus();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

}
