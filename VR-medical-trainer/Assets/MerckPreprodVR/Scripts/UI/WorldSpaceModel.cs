using Orbox.Async;
using System;
using UnityEngine;

namespace MerckPreprodVR
{
    public class WorldSpaceModel : BaseModel, IWorldSpaceModel
    {
        public event Action ButtonClicked = () => { };

        public WorldSpaceModel(IWorldSpaceView view, Transform target, Transform placeholder) : base(view, target, placeholder)
        {
            (_modelView as IWorldSpaceView).ButtonClicked += OnButtonClicked;
            _modelView.LookAtTransform(_target);
        }

        public override IPromise Hide()
        {
            CompositionRoot.GetSoundManager().Stop();
            return base.Hide();
        }

        private void OnButtonClicked()
        {
            ButtonClicked();
        }
    }
}
