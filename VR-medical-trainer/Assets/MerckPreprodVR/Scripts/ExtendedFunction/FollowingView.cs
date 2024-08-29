using DG.Tweening;
using UnityEngine;

namespace MerckPreprodVR
{
    public class FollowingView : BaseView, IFollowingView
    { 
        private Quaternion _defaultTurnOffset = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        protected virtual Quaternion TurnOffset => _defaultTurnOffset;
        private readonly float _distance = 1.5f;
        private readonly float _height = 0.5f;
        private readonly float _timeDelay = 1.5f;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;

        public virtual void SetTransform(Transform targetTransform)
        {
            UpdateTargetPosAndRotation(targetTransform);
            transform.position = _targetPosition;
            transform.rotation = _targetRotation; 

        }
        private void UpdateTargetPosAndRotation(Transform targetTransform)
        {
            _targetPosition = targetTransform.position + targetTransform.forward * _distance + targetTransform.up * _height;
            _targetRotation = targetTransform.rotation * TurnOffset;
        }

        public virtual void DoAnim(Transform targetTransform)
        {
            UpdateTargetPosAndRotation(targetTransform);
            transform.DORotate(_targetRotation.eulerAngles, _timeDelay).SetEase(Ease.OutQuart);
            transform.DOMove(_targetPosition, _timeDelay).SetEase(Ease.OutBack);
        }
    }
}