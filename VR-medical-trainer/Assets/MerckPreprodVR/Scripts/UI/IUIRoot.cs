using UnityEngine;

namespace MerckPreprodVR
{
    public interface IUIRoot
    {
        Transform WorldCanvas { get; }
        void WorldCanvasLookAt(Transform targetTransform);
    }
}