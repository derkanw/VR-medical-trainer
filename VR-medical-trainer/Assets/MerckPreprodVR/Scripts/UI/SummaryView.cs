using Orbox.Async;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MerckPreprodVR
{
    public class SummaryView : BaseView, ISummaryView
    {
        public event Action ContinueButtonClicked = () => { };
        public event Action DismissButtonClicked = () => { };

        [SerializeField] private Button ContinueButton;
        [SerializeField] private Button DismissButton;

        public void DisableButton(ESummaryButton name)
        {
            var button = name == ESummaryButton.ContinueButton ? ContinueButton : DismissButton;
            button.interactable = false;
        }

        public override IPromise Hide()
        {
            return base.Hide();
        }

        public override IPromise Show()
        {
            ContinueButton.onClick.AddListener(OnContinueButtonClicked);
            DismissButton.onClick.AddListener(OnDismissButtonClicked);
            return base.Show();
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
