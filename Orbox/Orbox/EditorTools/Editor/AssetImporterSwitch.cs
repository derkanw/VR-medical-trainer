using UnityEngine;
using UnityEditor;

namespace Orbox.EditorTools
{

    public class AssetImporterSwitch
    {
        [MenuItem("Orbox/Set Custom Asset Import", false, (int)MenuItemsPriority.AssetImportSwitch)]
        private static void CustomImport()
        {
            string key = EditorOprionsKeys.CustomAssetImport.ToString();
            EditorPrefs.SetBool(key, true);

            Debug.Log("Asset Import Settings was changed to custom values");
        }

        [MenuItem("Orbox/Set Defalult Asset Import", false, (int)MenuItemsPriority.AssetImportSwitch)]
        private static void DefaultImport()
        {
            string key = EditorOprionsKeys.CustomAssetImport.ToString();
            EditorPrefs.SetBool(key, false);

            Debug.Log("Asset Import Settings was changed to default values");
        }

        [MenuItem("Orbox/Set Custom Asset Import", true, (int)MenuItemsPriority.AssetImportSwitch)]
        private static bool CustomValidation()
        {
            string key = EditorOprionsKeys.CustomAssetImport.ToString();
            bool enabled = EditorPrefs.GetBool(key);

            return !enabled;
        }

        [MenuItem("Orbox/Set Defalult Asset Import", true, (int)MenuItemsPriority.AssetImportSwitch)]
        private static bool DefalutValidation()
        {
            string key = EditorOprionsKeys.CustomAssetImport.ToString();
            bool enabled = EditorPrefs.GetBool(key);

            return enabled;
        }
    }
}