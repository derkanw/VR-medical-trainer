using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public class TemperatureRoot : MonoBehaviour
    {
        [SerializeField] private Transform ThermometerTooltipPosition;
        [SerializeField] private Transform ThermometerPosition;
        [SerializeField] private Transform ThermometerSnapZonePosition;
        [SerializeField] private Transform ThermometerSnapZoneTooltipPosition;

        public Transform ThermometerTooltip()
        {
            return ThermometerTooltipPosition;
        }

        public Transform Thermometer()
        {
            return ThermometerPosition;
        }

        public Transform ThermometerSnapZone()
        {
            return ThermometerSnapZonePosition;
        }

        public Transform ThermometerSnapZoneTooltip()
        {
            return ThermometerSnapZoneTooltipPosition;
        }
    }
}
