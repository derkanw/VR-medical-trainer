using UnityEngine;

namespace MerckPreprodVR
{
    public class SummaryRoot : MonoBehaviour
    {
        [SerializeField] private Transform SummaryTooltipPosition;
        public Transform SummaryTooltip()
        {
            return SummaryTooltipPosition;
        }
    }
}
