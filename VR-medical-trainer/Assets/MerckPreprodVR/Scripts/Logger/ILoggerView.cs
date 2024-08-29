using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public interface ILoggerView 
    { 
        public void Log(string message);
        public void Enable();
        public void Disable();
        public void SetParent(Transform transform);
    }
}
