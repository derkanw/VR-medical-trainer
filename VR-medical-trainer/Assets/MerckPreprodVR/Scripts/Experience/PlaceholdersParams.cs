using UnityEngine;

namespace MerckPreprodVR
{
    [System.Serializable]
    public class PlaceholderParams<TEnum>
    {
        public TEnum Name;
        public Transform Place;
    }
}