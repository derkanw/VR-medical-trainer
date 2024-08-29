using UnityEngine;

namespace MerckPreprodVR
{
    public class WorldSpaceRoot : MonoBehaviour
    {
        [SerializeField] private Transform Position;

        public Transform GetPlaceholder()
        {
            return Position;
        }

    }
}
