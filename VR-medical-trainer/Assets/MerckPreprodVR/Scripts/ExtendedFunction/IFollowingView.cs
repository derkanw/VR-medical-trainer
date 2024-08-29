using UnityEngine;

namespace MerckPreprodVR
{
    public interface IFollowingView 
    {
        public void SetTransform(Transform transform);
        public void DoAnim(Transform transform);
    }
}