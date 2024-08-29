using UnityEditor;
using UnityEngine;

namespace Orbox.EditorTools
{

    public class AssetImporter : AssetPostprocessor
    {
        void OnPreprocessModel()
        {
            string key = EditorOprionsKeys.CustomAssetImport.ToString();
            bool enabled = EditorPrefs.GetBool(key);

            if(enabled)
            {
                ModelImporter modelImporter = assetImporter as ModelImporter;
                //modelImporter.globalScale = 1;
            
                //no collider or matirial
                modelImporter.addCollider = false;
                modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
            
                //no animation
                modelImporter.importAnimation = false;
                modelImporter.animationType = ModelImporterAnimationType.None;
                modelImporter.generateAnimations = ModelImporterGenerateAnimations.None;

#if UNITY_5
                modelImporter.importNormals = ModelImporterNormals.Import;
#else
                modelImporter.normalImportMode = ModelImporterTangentSpaceMode.Import;
#endif

                Debug.Log("Orbox.EditorTools change import settings. Importing model at: " + assetPath);
            }
        }
    }
}