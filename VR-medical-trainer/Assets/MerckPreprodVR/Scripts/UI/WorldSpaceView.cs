using Orbox.Async;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MerckPreprodVR
{
    public class WorldSpaceView : BaseView, IWorldSpaceView
    {       
        public event Action ButtonClicked = () => { };

        [SerializeField] private Button Button;

        public override IPromise Show()
        {
            Button.onClick.AddListener(OnButtonClicked);
            return base.Show();
        }

        public override IPromise Hide()
        {
            //Button.onClick.RemoveAllListeners();
            return base.Hide();
        }

        private void OnButtonClicked()
        {
            ButtonClicked();
        }
    }
}
