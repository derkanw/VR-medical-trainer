using UnityEngine;

namespace MerckPreprodVR
{
    public class MedicalHistoryRoot : MonoBehaviour
    {
        [SerializeField]
        private Transform MedicalHistoryTooltipPosition;
        [SerializeField]
        private Transform MedicalHistoryItemPosition;

        public Transform MedicalHistoryTooltip()
        {
            return MedicalHistoryTooltipPosition;
        }

        public Transform MedicalHistoryItem()
        {
            return MedicalHistoryItemPosition;
        }
    }
}
