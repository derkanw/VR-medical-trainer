using UnityEngine;

namespace MerckPreprodVR
{
    public class Placeholder : MonoBehaviour
    {

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}