﻿using System;
using System.Collections.Generic;
using Orbox.Async;
using UnityEngine;

namespace Orbox.Utils
{
    //orbox: TODO: there is a possibility to increase performance here. 
    public class Timers : MonoBehaviour , ITimers
    {       
        private List<Timer> FreeTimers = new List<Timer>();
        private List<Timer> UsedTimers = new List<Timer>();

        // --- unity ---
        private void Update()
        {
            for(int i = 0; i < UsedTimers.Count; i++)
                UsedTimers[i].Update();
        }

        // --- public ---
        public IPromise Wait(float seconds)
        {
            var timer = Obtain(seconds);
            return timer.Promise();
        }

        // --- private ---
        private class Timer
        {
            private float Delay;
            private float Elapsed;

            private Timers Owner;
            private Deferred Deferred = new Deferred();

            private int InitializeFrameCount = 0;

            public Timer(Timers owner)
            {
                Owner = owner;
            }

            public void Set(float seconds)
            {
                Delay = seconds;
                Elapsed = 0f;

                InitializeFrameCount = Time.frameCount;
            }

            public void Update()
            {
                if (InitializeFrameCount == Time.frameCount)
                    return;

                Elapsed += Time.deltaTime;

                if (Elapsed >= Delay)
                {
                    try
                    {
                        Deferred.Resolve();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Timer Deferred Resolve Exception: " + e.Message);
                    }
                    finally
                    {
                        Deferred = new Deferred(); //orbox: TODO: Use Deferred Pool
                        Owner.Release(this);
                    }
                }
            }

            public IPromise Promise()
            {
                return Deferred.Promise();
            }
        }

        private Timer Obtain(float seconds)
        {
            Timer timer;

            if (FreeTimers.Count > 0)
            {
                // Obtain timer instance from the pool
                timer = FreeTimers[FreeTimers.Count - 1];
                FreeTimers.RemoveAt(FreeTimers.Count - 1);
            }
            else
            {
                // Create new instance
                timer = new Timer(this);
            }

            timer.Set(seconds);
            UsedTimers.Add(timer);

            return timer;
        }

        private void Release(Timer timer)
        {
            UsedTimers.Remove(timer);
            FreeTimers.Add(timer);
        }
    }
}