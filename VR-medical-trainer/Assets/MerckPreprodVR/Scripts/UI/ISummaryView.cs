using System;

namespace MerckPreprodVR
{
    public interface ISummaryView : IView
    {
        event Action ContinueButtonClicked;
        event Action DismissButtonClicked;

        void DisableButton(ESummaryButton name);
    }
}