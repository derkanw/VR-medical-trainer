using System;
using System.Collections;
using System.Collections.Generic;
using MerckPreprodVR;
using Orbox.Async;
using UnityEngine;

namespace MerckPreprodVR
{
    public class ResearchWindowModel : BaseModel, IResearchWindowModel
    {
        public event Action ContinueButtonClicked = () => { };
        public event Action SummaryButtonClicked = () => { };
        public ResearchWindowModel(IResearchWindowView view, Transform target, Transform placeholder) : base(view, target, placeholder)
        {
            (_modelView as IResearchWindowView).ContinueButtonClicked += OnContinueButtonClicked;
            (_modelView as IResearchWindowView).SummaryButtonClicked += OnSummaryButtonClicked;
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
    
        private void OnSummaryButtonClicked()
        {
            SummaryButtonClicked();
        }
    }
}

