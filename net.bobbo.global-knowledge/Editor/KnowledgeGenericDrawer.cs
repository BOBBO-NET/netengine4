using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(KnowledgeGeneric<>), useForChildren: true)]
    public class KnowledgeGenericDrawer : PropertyDrawer
    {
        private const int lineCount = 2;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = base.GetPropertyHeight(property, label);
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            Rect labelRect = position;
            labelRect.height = lineHeight;
            labelRect = EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            float indentStart = labelRect.x;
            float indentWidth = labelRect.width;
            EditorGUI.PropertyField(labelRect, property, GUIContent.none);

            // Draw the current value
            var knowledge = EditorHelper.GetTargetObjectOfProperty(property) as IKnowledge;
            Rect valueRect = position;
            valueRect.x = indentStart;
            valueRect.width = indentWidth;
            valueRect.height = lineHeight;
            valueRect.y += lineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.LabelField(valueRect, $"Current value: {knowledge?.GetCurrentValue()}", EditorStyles.helpBox);

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * lineCount;
        }
    }
}