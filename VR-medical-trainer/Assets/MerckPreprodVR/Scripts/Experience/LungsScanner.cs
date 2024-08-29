using System;
using UnityEngine;
using UnityEngine.UI;

namespace MerckPreprodVR
{
    public class LungsScanner : MonoBehaviour
    {
        public event Action ScannerUpdated = () => { };
        public Text labelToUpdate;
        [SerializeField] private Highlight _highlight;
        private float _sliderPercent;

        public void SetPosition(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }

        public void Highlight()
        {
            _highlight.EnableBlinking(true);
        }

        public void UpdateSliderText(float sliderValue)
        {
            _sliderPercent = sliderValue / 100;
            labelToUpdate.text = sliderValue + "%";
            ScannerUpdated();
        }

        public float GetSliderPercent()
        {
            return _sliderPercent;
        }
    }
}