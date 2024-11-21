using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace UI
{
    [CustomEditor(typeof(OrientationController))]
    [CanEditMultipleObjects]
    public class OrientationControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.Label("Current orientation: " +
                (OrientationController.IsVertical ? "Vertical" : "Horizontal"));

            DrawDefaultInspector();

            var controllers = targets;

            if (GUILayout.Button("Save values"))
                foreach (var controller in controllers)
                    ((OrientationController)controller).SaveCurrentState();

            if (GUILayout.Button("Put values"))
                foreach (var controller in controllers)
                    ((OrientationController)controller).PutCurrentState();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}