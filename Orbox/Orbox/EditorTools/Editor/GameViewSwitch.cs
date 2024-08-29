using UnityEngine;
using UnityEditor;

namespace Orbox.EditorTools
{



    public class GameViewSwitch
    {
        [MenuItem("Orbox/Set Focus on Scene View")]
        private static void SceneFocus()
        {
            string key = EditorOprionsKeys.FocusOnSceneWindow.ToString();
            EditorPrefs.SetBool(key, true);

            Debug.Log("SceneFocus");
        }

        [MenuItem("Orbox/Set Focus on Game View")]
        private static void GameFocus()
        {
            string key = EditorOprionsKeys.FocusOnSceneWindow.ToString();
            EditorPrefs.SetBool(key, false);

            Debug.Log("GameFocus");
        }

        [MenuItem("Orbox/Set Focus on Scene View", true)]
        private static bool SceneFocusValidation()
        {
            string key = EditorOprionsKeys.FocusOnSceneWindow.ToString();
            bool enabled = EditorPrefs.GetBool(key);

            return !enabled;
        }

        [MenuItem("Orbox/Set Focus on Game View", true)]
        private static bool GameFocusValidation()
        {
            string key = EditorOprionsKeys.FocusOnSceneWindow.ToString();
            bool enabled = EditorPrefs.GetBool(key);

            return enabled;
        }
    }
}