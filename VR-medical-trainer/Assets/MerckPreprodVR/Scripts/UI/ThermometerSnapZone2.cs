using UnityEngine;
using System;
using Orbox.Async;
using DG.Tweening;

namespace MerckPreprodVR
{
    public class ThermometerSnapZone2 : MonoBehaviour, ISnapZone
    {
        public event Action InSnapZone = () => { };
        public event Action OutSnapZone = () => { };

        [SerializeField] private float _fadeTime = 0.1f;
        [SerializeField] private GameObject _insideModel;
        [SerializeField] private GameObject _outsideModel;

        public void OnTriggerEnter(Collider other)
        {
            if(InSnapZone != null)
            {
                if (other.gameObject.CompareTag("TargetItem"))
                {
                    SwitchModels(true);
                    InSnapZone();
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (OutSnapZone != null)
            {
                if (other.gameObject.CompareTag("TargetItem"))
                {
                    SwitchModels(false);
                    OutSnapZone();
                }
            }
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }
        
        public void SetPosition(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        
        public IPromise Show()
        {
            //return FadeIn(0.3f, _fadeTime);
            SwitchModels(false);
            return new Deferred().Resolve();
        }

        public IPromise Hide()
        {
            //return FadeOut(0f, _fadeTime);
            _insideModel.SetActive(false);
            _outsideModel.SetActive(false);
            return new Deferred().Resolve();
        }
        
        public void Enable()
        {
            gameObject.SetActive(true);
            _insideModel.SetActive(false);
            _outsideModel.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        //private IPromise FadeIn(float alpha, float duration)
        //{
        //    var promise = new Deferred();
        //    _snapZoneModel.SetActive(true);
        //    _material.DOFade(alpha, duration).OnComplete(() =>
        //    {
        //        promise.Resolve();
        //    });

        //    return promise;
        //}

        //private IPromise FadeOut(float alpha, float duration)
        //{
        //    var promise = new Deferred();
        //    _material.DOFade(alpha, duration).OnComplete(() =>
        //    {
        //        _snapZoneModel.SetActive(false);
        //        promise.Resolve();
        //    });

        //    return promise;
        //}

        public void ClearEvents()
        {
            InSnapZone = null;
            OutSnapZone = null;
        }

        public void SwitchModels(bool inside)
        {
            _insideModel.SetActive(inside);
            _outsideModel.SetActive(!inside);
        }
    }
}
