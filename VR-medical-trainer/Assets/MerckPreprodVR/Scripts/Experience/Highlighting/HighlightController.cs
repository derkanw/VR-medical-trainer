using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public class HighlightController : MonoBehaviour, IHighlightController
    {
        public List<EPhases> phases;
        public List<Highlight> highlightItems;

        private Dictionary<EPhases, IHighlight> phaseItems;

        public void Start()
        {
            int i = 0;
            foreach(EPhases phase in phases)
            {
                phaseItems.Add(phase, highlightItems[i]);
                i++;
            }
        }

        public void HighlightItem(EPhases phase)
        {
            phaseItems[phase].EnableOutline(true);
        }

        public void UnhighlightItem(EPhases phase)
        {
            phaseItems[phase].EnableOutline(false);
        }
    }
}
