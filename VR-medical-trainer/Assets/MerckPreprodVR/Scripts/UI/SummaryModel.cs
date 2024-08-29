using Orbox.Async;
using System;
using UnityEngine;

namespace MerckPreprodVR
{
    public class SummaryModel : BaseModel, ISummaryModel
    {
        public event Action ContinueButtonClicked = () => { };
        public event Action DismissButtonClicked = () => { };
        public SummaryModel(ISummaryView view, Transform target, Transform placeholder) : base(view, target, placeholder)
        {
            (_modelView as ISummaryView).ContinueButtonClicked += OnContinueButtonClicked;
            (_modelView as ISummaryView).DismissButtonClicked += OnDismissButtonClicked;
        }

        public void DisableButton(ESummaryButton name)
        {
            (_modelView as ISummaryView).DisableButton(name);
        }

        public override IPromise Hide()
        {
            CompositionRoot.GetSoundManager().Stop();
            return base.Hide();
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonClicked();
        }

        private void OnDismissButtonClicked()
        {
            DismissButtonClicked();
        }
    }
}