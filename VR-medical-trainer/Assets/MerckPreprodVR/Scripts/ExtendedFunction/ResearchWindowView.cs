using System;
using System.Collections;
using System.Collections.Generic;
using MerckPreprodVR;
using Orbox.Async;
using UnityEngine;
using UnityEngine.UI;

namespace MerckPreprodVR
{
    public class ResearchWindowView : BaseView, IResearchWindowView
    {
        public event Action ContinueButtonClicked = () => { };
        public event Action SummaryButtonClicked = () => { };
    
        [SerializeField] private Button ContinueButton;
        [SerializeField] private Button SummaryButton;
    
        public override IPromise Hide()
        {
            ContinueButton.onClick.RemoveAllListeners();
            SummaryButton.onClick.RemoveAllListeners();
            return base.Hide();
        }
    
        private void Awake()
        {
            ContinueButton.onClick.AddListener(OnContinueButtonClicked);
            SummaryButton.onClick.AddListener(OnSummaryButtonClicked);
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
