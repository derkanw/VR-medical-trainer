using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface IHighlight
    {
        void EnableOutline(bool outline);
        void EnableBlinking(bool blink);
        void Targeted(bool onTarget);
    }
}
