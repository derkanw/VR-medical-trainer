using UnityEngine;

namespace MerckPreprodVR
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Transform WorldCanvasTrasnsform; //set in editor
        public Transform WorldCanvas => WorldCanvasTrasnsform; //realisation of property

        private const float DefaultDistance = 3f;

        public void WorldCanvasLookAt(Transform targetTransform)
        {
            var rotation = Quaternion.Euler(0f, targetTransform.rotation.eulerAngles.y, 0f);
            var position = targetTransform.position;
            var forward = rotation * Vector3.forward;

            WorldCanvas.transform.position = position + forward * DefaultDistance;
            WorldCanvas.transform.rotation = Quaternion.LookRotation(WorldCanvas.transform.position - position);
        }
    }
}