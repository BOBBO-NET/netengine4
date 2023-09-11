using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;
using System.Linq;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(KnowledgeCaseGenericLessThan<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(KnowledgeCaseGenericGreaterThan<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(KnowledgeCaseGenericEquals<>), useForChildren: true)]
    [CustomPropertyDrawer(typeof(KnowledgeCaseGenericEqualsKnowledge<>), useForChildren: true)]
    public class KnowledgeCaseGenericValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = base.GetPropertyHeight(property, label);
            EditorGUI.BeginProperty(position, label, property);

            // Draw Value Field
            Rect labelRect = position;
            labelRect.height = lineHeight;
            labelRect = EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(labelRect, property.FindPropertyRelative("Value"), GUIContent.none);

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value"), label);
        }
    }
}