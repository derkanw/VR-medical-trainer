using UnityEngine;

using System;
using System.Collections;

namespace Orbox.Utils
{
    public enum TimerType { NOOP, Regular, Once }

    public class UpdateTimer
    {
        public TimerType Type;

        private float Timer;
        private float Interval;

        private Action NOOP = () => { };
        private Action Action = ()=> { };
               

        private class MonoTimer: MonoBehaviour
        {
            public Action UpdateAction = ()=> { };

            public void Update()
            {
                UpdateAction();
            }
        }

        public UpdateTimer(GameObject host)
        {
            var monoTimer = host.AddComponent<MonoTimer>();
            monoTimer.UpdateAction = DoUpdate;
        }


        public void SetNOOP()
        {
            Type = TimerType.NOOP;
            Action = NOOP;
        }

        public void SetOnce(float interval, Action action)
        {
            Type = TimerType.Once;            
            Interval = interval;
            Action = action;
            Timer = 0;
        }

        public void SetRegular(float interval, Action action)
        {
            Type = TimerType.Regular;
            Interval = interval;
            Action = action;
            Timer = 0;
        }

        //--- Private ---
        private void DoUpdate()
        {
            switch (Type)
            {
                case TimerType.NOOP: break;
                case TimerType.Once: UpdateOnce(); break;
                case TimerType.Regular: UpdateRegular(); break;
            }

        }

        private void UpdateRegular()
        {
            Timer += Time.deltaTime;

            if (Timer > Interval)
            {
                Timer = 0;
                Action();
            }
        }

        private void UpdateOnce()
        {
            Timer += Time.deltaTime;

            if (Timer > Interval)
            {
                Type = TimerType.NOOP;
                Timer = 0;         
                Action();                
            }
        }

    }
}
