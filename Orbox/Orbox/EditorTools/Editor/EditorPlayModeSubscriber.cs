using UnityEngine;
using UnityEditor;

namespace Orbox.EditorTools
{
    [InitializeOnLoad]
    public class EditorPlayModeSubscriber
    {
        [SerializeField]
        public static bool IsEnabled;

        static EditorPlayModeSubscriber()
        {
            Debug.Log("EditorPlayModeSubscriber. Up and running");
            EditorPlayMode.PlayModeChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeState currentMode, PlayModeState changedMode)
        {
            bool focusOnScene = EditorPrefs.GetBool(EditorOprionsKeys.FocusOnSceneWindow.ToString());

            if(focusOnScene) 
                SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));

        }


    }
}

