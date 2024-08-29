using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace MerckPreprodVR
{
    public class Highlight : MonoBehaviour, IHighlight
    {
        //[SerializeField] private bool targeted; – uncomment if you want to test targeting
        [SerializeField] private bool outlined;
        [SerializeField] private bool blinking;
        //Prototype material with outline shader.
        [SerializeField] private Material prototypeOutlineMaterial;
        [SerializeField] private Color outlineColor;
        [SerializeField] private Color targetedOutlineColor;
        // Half-amplitude of object blinking.
        [SerializeField] [Range(0, 5)]
        private float blinkLength = 0.5f;
        
        private Material[] _outlineMaterials;
        // Save all the child-objects renderers.
        private MeshRenderer[] _renderers;
        // Saves origin materials to return it to object after outline stops.
        private Material[] _originMaterials;    
        // Saves the origin texture give it to outline material.
        private Texture[] _originTextures;
        private Tween[] _tweens;
        private Color _blinkColor;

        public void Awake()
        {
            // Collecting all renderers from child objects.
            _renderers = GetComponentsInChildren<MeshRenderer>(true);
            if(_renderers == null)
            {
                Debug.Log("Error! Object has no renderers!");
                return;
            }
            // Saving origin materials and textures from all objects.
            SaveOriginalMaterials();
            CreateOutlineMaterials();

            // Start highlighting (ONLY FOR TESTING!!!)
            /*if (outlined)
            { 
                EnableOutline(true);
            } else if (blinking)
            {
                EnableBlinking(true);  
            }*/

        }

        void Update()
        {
            //Targeted(targeted); – uncomment if you want to test targeting
        }

        private void SaveOriginalMaterials()
        {
            _originMaterials = new Material[_renderers.Length]; 
            _originTextures = new Texture[_renderers.Length]; 
            for (int i = 0; i < _renderers.Length; i++)
            {
                _originMaterials[i] = _renderers[i].material;
                _originTextures[i] = _renderers[i].material.mainTexture;
            }
        }

        private void CreateOutlineMaterials()
        {
            if (prototypeOutlineMaterial == null) return;
            _outlineMaterials = new Material[_originMaterials.Length];
            for (int i = 0; i < _renderers.Length; i++)
            {
                _outlineMaterials[i] = new Material(prototypeOutlineMaterial)
                {
                    mainTexture = _originTextures[i]
                };
                _outlineMaterials[i].SetColor("_OutlineColor",
                    outlineColor);
            }
        }

        // Method for turning outlining on/off.
        public void EnableOutline(bool outline)     //O
        {
            if (outline)
            {
                // Enables outlines for all materials with correct parameters.
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material = _outlineMaterials[i];
                    _renderers[i].material.SetColor("_OutlineColor", outlineColor);
                }
            }
            else
            {
                //Returning origin and default settings for materials.
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material.SetColor("_OutlineColor", outlineColor);
                    _renderers[i].material = _originMaterials[i];
                }
            }
        }

        // Method for turning blinking on/off.
        public void EnableBlinking(bool blink)     
        {
            if (blink)
            {
                if (_tweens != null) return;
                _tweens = new Tween[_renderers.Length];
                _blinkColor = outlineColor;
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material.color = Color.white;
                    _tweens[i] = _renderers[i].material.DOColor(_blinkColor, blinkLength).SetLoops(-1, LoopType.Yoyo);
                }
            }
            else
            {
                if (_tweens == null) return;
                foreach (var tween in _tweens)
                {
                    tween.Kill();
                }
                foreach (var meshRenderer in _renderers)
                {
                    meshRenderer.material.color = Color.white;
                }

                _tweens = null;
            }
        }

        // Method for turning targeting on/off when player looks at object.
        public void Targeted(bool onTarget)
        {
            if (outlined)    
            {
                if (onTarget)
                {
                    foreach (var meshRenderer in _renderers)
                    {
                        meshRenderer.material.SetColor("_OutlineColor", targetedOutlineColor);
                    }
                }
                else
                {
                    foreach (var meshRenderer in _renderers)
                    {
                        meshRenderer.material.SetColor("_OutlineColor", outlineColor);
                    }
                }
            }
            else if (blinking)
            {
                if (onTarget)
                {
                   EnableBlinking(false);
                   foreach (var meshRenderer in _renderers)
                   {
                       meshRenderer.material.color = targetedOutlineColor;
                   }
                }
                else
                {
                    EnableBlinking(true);
                }
            }
        }
        
    }
}
