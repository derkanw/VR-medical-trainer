using System.Collections;
using System.Collections.Generic;
using Orbox.Async;
using Orbox.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace MerckPreprodVR
{

    public class TVView : BaseView, ITVView
    {
        [SerializeField]
        private List<VideoClip> playlist = new List<VideoClip>();
        /*[SerializeField]
        private List<Texture> textures = new List<Texture>();*/
        [SerializeField]
        private VideoPlayer videoPlayer;
        [SerializeField]
        private RawImage rawImage;
        [SerializeField]
        private RawImage rawImageTextures;

        private Object[] _textures;
        private ITimers _timers;
        private Texture _originTexture;

        private void Awake()
        {
            _textures = Resources.LoadAll("LUNGS", typeof(Texture));
            _timers = CompositionRoot.GetTimers();
            _originTexture = rawImage.texture;
        }

        // Method for playing single video
        public void PlayVideo(int num)
        {
            if (num >= playlist.Count)
            {
                Debug.Log("no video " + num.ToString() + " in playlist");
                CompositionRoot.GetLogger().LogError("no video " + num.ToString() + " in playlist");
                return;
            }
            IEnumerator coroutine = PlayOne(num);
            StartCoroutine(coroutine);
        }

        // Method for playing two videos one after another
        public void PlayVideo(int a, int b)
        {
            if (a >= playlist.Count || a >= playlist.Count)
            {
                Debug.Log("no video " + a.ToString() + " and " + b.ToString() + " in playlist");
                CompositionRoot.GetLogger().LogError("no video " + a.ToString() + " and " + b.ToString() + " in playlist");
                return;
            }
            IEnumerator coroutine = PlayTwo(a, b);
            StartCoroutine(coroutine);
        }
        
        public IPromise PlayClip(int i)
        {
            rawImageTextures.gameObject.SetActive(false);
            rawImage.gameObject.SetActive(true);
            rawImage.texture = _originTexture;
            VideoClip clip = playlist[i];
            videoPlayer.clip = clip;
            videoPlayer.Play();
            return _timers.Wait((float) clip.length);
        }

        public IPromise PlayLogo()
        {
            return PlayClip(0);
        }
        
        public IPromise PlayIntro()
        {
            return PlayClip(1);
        }

        public IPromise PlayMedicalHistoryPhaseStart()
        {
            return PlayClip(2);
        }
        
        public IPromise PlayMedicalHistoryPhaseEnd()
        {
            return PlayClip(3);
        }
        
        public IPromise PlayTemperaturePhaseStart()
        {
            return PlayClip(4);
        }
        
        public IPromise PlayTemperaturePhaseEnd()
        {
            return PlayClip(5);
        }
        
        public IPromise PlaySaturationPhaseStart()
        {
            return PlayClip(6);
        }
        
        public IPromise PlaySaturationPhaseEnd()
        {
            return PlayClip(7);
        }
        
        public IPromise PlayCTPhaseStart()
        {
            return PlayClip(8);
        }
        
        public IPromise PlayCTPhaseEnd()
        {
            return PlayClip(9);
        }
        
        public IPromise PlaySummaryPhaseStart()
        {
            return PlayClip(10);
        }
        
        public IPromise PlaySummaryPhaseEnd()
        {
            return PlayClip(11);
        }

        public IEnumerator PlayOne(int num)
        {
            videoPlayer.clip = playlist[num];
            videoPlayer.Play();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }
        

        public IEnumerator PlayTwo(int a, int b)
        {
            videoPlayer.clip = playlist[a];
            videoPlayer.Play();
            while (!(videoPlayer.frame > 0 && !videoPlayer.isPlaying))
            {
                yield return null;
            }

            videoPlayer.clip = playlist[b];
            videoPlayer.Play();
            while (!(videoPlayer.frame > 0 && !videoPlayer.isPlaying))
            {
                yield return null;
            }
        }

        public void ShowImage(float num)
        {
            rawImage.gameObject.SetActive(false);
            rawImageTextures.gameObject.SetActive(true);
            rawImageTextures.texture = (Texture)_textures[(int) ((_textures.Length - 1) * num)];
        }
    }

}
