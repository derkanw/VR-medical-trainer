using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public class Tooltip : BaseModel, ITooltip
    {
        public Tooltip(ITooltipView tooltipView, Transform target, Transform placeholder) : base(tooltipView, target, placeholder) { }

        public void Update()
        {
            _modelView.LookAtTransform(_target);
        }

        public IPromise Complete()
        {
            SetCheckMark(true);
            return Hide();
        }

        public void Reactivate()
        {
            SetCheckMark(false);
        }

        private void SetCheckMark(bool value)
        {
            (_modelView as ITooltipView).SetCheckMark(value);
        }
    }
}