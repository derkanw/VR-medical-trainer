using UnityEngine;

namespace MerckPreprodVR
{
    public class TooltipView : BaseView, ITooltipView
    {
        public GameObject CheckMark;

        public void SetCheckMark(bool value)
        {
            CheckMark.SetActive(value);
        }
    }
}
