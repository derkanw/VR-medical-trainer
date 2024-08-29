using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface IView
    {
        void SetParent(Transform parent);
        void SetPosition(Transform newPosition);
        void LookAtTransform(Transform targetTransform);
        void Enable();
        void Disable();
        IPromise Show();
        IPromise Hide();
    }
}