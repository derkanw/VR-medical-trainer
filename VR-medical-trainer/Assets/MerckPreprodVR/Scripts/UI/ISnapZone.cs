using System;
using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface ISnapZone
    {
        public event Action InSnapZone;
        public event Action OutSnapZone;
        public void SetParent(Transform parent);
        public void SetPosition(Transform target);
        public IPromise Show();
        public IPromise Hide();
        public void SwitchModels(bool inside);
        public void Enable();
        public void Disable();
        public void ClearEvents();
    }
}
