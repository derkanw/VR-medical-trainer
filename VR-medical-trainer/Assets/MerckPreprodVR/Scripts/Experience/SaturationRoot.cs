using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public class SaturationRoot : MonoBehaviour
    {
        [SerializeField] private Transform SaturationTooltipPosition;
        [SerializeField] private Transform PulseoximeterPosition;
        [SerializeField] private Transform PulseoximeterSnapZonePosition;
        [SerializeField] private Transform PulseoximeterSnapZoneTooltipPosition;

        public Transform SaturationTooltip()
        {
            return SaturationTooltipPosition;
        }

        public Transform Pulseoximeter()
        {
            return PulseoximeterPosition;
        }

        public Transform PulseoximeterSnapZone()
        {
            return PulseoximeterSnapZonePosition;
        }

        public Transform PulseoximeterSnapZoneTooltip()
        {
            return PulseoximeterSnapZoneTooltipPosition;
        }
    }
}
