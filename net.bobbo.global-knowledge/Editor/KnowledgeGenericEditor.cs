using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    [CustomEditor(typeof(KnowledgeGeneric<>), editorForChildClasses: true)]
    public class KnowledgeGenericEditor : UnityEditor.Editor
    {
        //
        //  Editor Methods
        //

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Draw the current value
            var knowledge = target as IKnowledge;
            EditorGUILayout.LabelField($"Current value: {knowledge.GetCurrentValue()}");

            // Mark to repaint immediately, so we can see the current value update in realtime
            this.Repaint();
        }
    }
}