using BNG;
using System;
using UnityEngine;

namespace MerckPreprodVR
{
    public class MedicalHistoryTablet : MonoBehaviour
    {
        public event Action Grabbed = () => { };
        [SerializeField] private Highlight _highlight;
        [SerializeField] private Grabbable _grabbable;

        public void Highlight()
        {
            _highlight.EnableBlinking(true);
        }

        public void Unhighlight()
        {
            _highlight.EnableBlinking(false);
        }

        public void SetPosition(Transform target)
        {
            transform.position = target.position;
        }

        private void Update()
        {
            if (_grabbable.BeingHeld)
            {
                Grabbed();
                gameObject.SetActive(false);
            }
        }
    }
}
