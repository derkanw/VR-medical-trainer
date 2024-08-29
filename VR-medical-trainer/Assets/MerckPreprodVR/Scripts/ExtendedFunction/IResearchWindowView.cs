using System;
using System.Collections;
using System.Collections.Generic;
using MerckPreprodVR;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface IResearchWindowView : IView
    {
        event Action ContinueButtonClicked;
        event Action SummaryButtonClicked;
    }
}

