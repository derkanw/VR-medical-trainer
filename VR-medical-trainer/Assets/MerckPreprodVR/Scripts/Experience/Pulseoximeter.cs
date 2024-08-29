using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BNG;

namespace MerckPreprodVR
{
    public class Pulseoximeter : MonoBehaviour
    {
        public event Action Grabbed = () => { };
        public event Action Dropped = () => { };
        [SerializeField] private Highlight _highlight;
        [SerializeField] private Grabbable _grabbable;

        [SerializeField] private ReturnToSnapZone snapZone;
        [SerializeField] private SnapZone startZone;
        [SerializeField] private SnapZone measureZone;

        private bool _isHeld = false;

        private void OnEnable()
        {
            SwitchZones(false);
        }

        private void Update()
        {
            if (Dropped != null && Grabbed != null)
            {
                if (_isHeld)
                {
                    if (!_grabbable.BeingHeld)
                    {
                        _isHeld = false;
                        Dropped();
                    }
                }
                else
                {
                    if (_grabbable.BeingHeld)
                    {
                        _isHeld = true;
                        Grabbed();
                    }
                }
            }
        }

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
            transform.rotation = target.rotation;
        }

        public void SwitchZones(bool process)
        {
            if (process == false)
            {
                snapZone.ReturnTo = startZone;
                snapZone.ReturnDelay = 2f;
            }
            if (process == true)
            {
                snapZone.ReturnTo = measureZone;
                snapZone.ReturnDelay = 0f;
            }
        }

        public void ClearEvents()
        {
            Grabbed = null;
            Dropped = null;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
