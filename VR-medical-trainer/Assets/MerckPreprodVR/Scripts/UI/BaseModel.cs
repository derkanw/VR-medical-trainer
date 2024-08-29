using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public abstract class BaseModel : IModel
    {
        protected IView _modelView;
        protected Transform _target;
        protected Transform _placeholder;

        protected BaseModel(IView view, Transform target, Transform placeholder)
        {
            _modelView = view;
            _target = target;
            _placeholder = placeholder;

            _modelView.SetPosition(_placeholder);
            _modelView.Disable();
        }

        public virtual IPromise Show()
        {
            _modelView.LookAtTransform(_target);
            return _modelView.Show();
        }

        public virtual IPromise Hide()
        {
            return _modelView.Hide();
        }
    }
}