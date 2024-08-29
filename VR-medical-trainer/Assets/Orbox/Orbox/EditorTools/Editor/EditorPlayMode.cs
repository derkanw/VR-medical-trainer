using UnityEngine;
using UnityEditor;
using System;


namespace Orbox.EditorTools
{
    public enum PlayModeState
    {
        Stopped,
        Playing,
        Paused
    }

    [InitializeOnLoad]
    public class EditorPlayMode
    {
        public static event Action<PlayModeState, PlayModeState> PlayModeChanged;

        private static PlayModeState PlayModeCurrent = PlayModeState.Stopped;
        

        static EditorPlayMode()
        {
            EditorApplication.playmodeStateChanged = OnUnityPlayModeChanged;
        }
        
        public static void Play()
        {
            EditorApplication.isPlaying = true;
        }

        public static void Pause()
        {
            EditorApplication.isPaused = true;
        }

        public static void Stop()
        {
            EditorApplication.isPlaying = false;
        }

        private static void OnPlayModeChanged(PlayModeState current, PlayModeState @new)
        {
            if (PlayModeChanged != null)
                PlayModeChanged(current, @new);
        }

        private static void OnUnityPlayModeChanged()
        {
            var playModeNew = PlayModeState.Stopped;

            switch (PlayModeCurrent)
            {
                case PlayModeState.Stopped:

                    if (EditorApplication.isPlayingOrWillChangePlaymode)
                        playModeNew = PlayModeState.Playing;
                    break;

                case PlayModeState.Playing:

                    if (EditorApplication.isPaused)                    
                        playModeNew = PlayModeState.Paused;
                    else
                        playModeNew = PlayModeState.Stopped;
                    break;

                case PlayModeState.Paused:

                    if (EditorApplication.isPlayingOrWillChangePlaymode)
                        playModeNew = PlayModeState.Playing;
                    else
                        playModeNew = PlayModeState.Stopped;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            var from = PlayModeCurrent;
            PlayModeCurrent = playModeNew;

            // Fire PlayModeChanged event.
            OnPlayModeChanged(from, playModeNew);            
        }
    }
}