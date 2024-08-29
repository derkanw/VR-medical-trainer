using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{

    public interface IVRCameraRig
    {
        Transform PlayerTransform { get; }
    }
}