using System;

namespace MerckPreprodVR
{
    public interface ISummaryModel : IModel
    {
        event Action ContinueButtonClicked;
        event Action DismissButtonClicked;

        void DisableButton(ESummaryButton name);
    }
}