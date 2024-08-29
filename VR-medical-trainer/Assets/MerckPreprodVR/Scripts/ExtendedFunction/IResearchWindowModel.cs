using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface IResearchWindowModel : IModel
    {
        event Action ContinueButtonClicked;
        event Action SummaryButtonClicked;
    }
}

