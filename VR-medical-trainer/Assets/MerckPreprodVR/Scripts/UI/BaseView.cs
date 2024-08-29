using DG.Tweening;
using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        public CanvasGroup cg;
        public float fadeTime;
        public Vector3 Rotation;

        private Quaternion _rotationOffset;

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
            _rotationOffset = Rotation != Vector3.zero ? Quaternion.Euler(Rotation) : Quaternion.Euler(Vector3.one);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public virtual IPromise Show()
        {
            cg.alpha = 0f;
            return FadeIn(1f, fadeTime);
        }

        public virtual IPromise Hide()
        {
            return FadeOut(0f, fadeTime);
        }

        private IPromise FadeIn(float alpha, float duration)
        {
            var promise = new Deferred();
            Enable();
            cg.DOFade(alpha, duration).OnComplete(() =>
            {
                promise.Resolve();
            });

            return promise;
        }

        private IPromise FadeOut(float alpha, float duration)
        {
            var promise = new Deferred();
            cg.DOFade(alpha, duration).OnComplete(() =>
            {
                Disable();
                promise.Resolve();
            });

            return promise;
        }

        public void SetPosition(Transform newPosition)
        {
            if (newPosition == null) return;
            transform.position = newPosition.position;
        }

        public void LookAtTransform(Transform targetTransform)
        {
            if (targetTransform == null) return;
            var newPosition = transform.position - targetTransform.position;
            transform.rotation = Quaternion.LookRotation(newPosition);
            var angles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, angles.y, 0f) * _rotationOffset;
        }
    }
}