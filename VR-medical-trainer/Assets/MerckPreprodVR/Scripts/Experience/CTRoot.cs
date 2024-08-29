using UnityEngine;

namespace MerckPreprodVR
{
    public class CTRoot : MonoBehaviour
    {
        [SerializeField] private Transform researchWindow;
        [SerializeField] private Transform ctTooltip;
        [SerializeField] private Transform lungsScannerPosition;

        public Transform ResearchWindow()
        {
            return researchWindow;
        }

        public Transform CtTooltip()
        {
            return ctTooltip;
        }
        
        public Transform LungsScanner()
        {
            return lungsScannerPosition;
        }
    }
}