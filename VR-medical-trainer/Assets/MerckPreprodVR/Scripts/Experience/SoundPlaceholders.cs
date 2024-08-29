using Orbox.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MerckPreprodVR
{
    public class SoundPlaceholders : MonoBehaviour
    {
        // this dictionary contains the matching sound name (enum key) and placeholder Transform 
        [SerializeField] private List<PlaceholderParams<ESpatial>> PlacesList;
        private ISoundManager _soundManager;
        private IResourceManager _resourceManager;

        private void Awake()
        {
            _soundManager = CompositionRoot.GetSoundManager();
            _resourceManager = CompositionRoot.GetResourceManager();
            InitSounds<ESpatial>();
        }

        // to warm up and move sound object to the matching position
        private void InitSounds<TEnum>()
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            foreach (TEnum soundName in Enum.GetValues(typeof(TEnum)))
            {
                var place = GetPlace(soundName);
                if (place == null) continue;
                var soundPrefab = _resourceManager.GetPrefab(soundName).transform;
                var originPosition = soundPrefab.position;

                soundPrefab.position = place.position;
                _soundManager.Play(soundName);
                _soundManager.Stop(soundName);
                soundPrefab.position = originPosition;
            }
        }

        // to get the necessary Transform according to the sound name
        private Transform GetPlace<TEnum>(TEnum soundName)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            foreach (var placeholder in PlacesList)
                if (Enum.IsDefined(typeof(TEnum), placeholder.Name) && EnumInt32ToInt.Convert(placeholder.Name) == EnumInt32ToInt.Convert(soundName))
                    return placeholder.Place;
            return null;
        }
    }
}