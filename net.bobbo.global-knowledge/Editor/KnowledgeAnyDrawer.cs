using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;
using System.Linq;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(KnowledgeAny), useForChildren: true)]
    public class KnowledgeAnyDrawer : PropertyDrawer
    {
        private const int lineCount = 2;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = base.GetPropertyHeight(property, label);
            var knowledgeAny = EditorHelper.GetTargetObjectOfProperty(property) as KnowledgeAny;
            EditorGUI.BeginProperty(position, label, property);

            Rect labelRect = position;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            labelRect = EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            float indentStart = labelRect.x;
            float indentWidth = labelRect.width;

            // Draw the field for the target knowledge
            var resolvedKnowledge = knowledgeAny.Get();
            ScriptableKnowledgeObject selectedObject = (ScriptableKnowledgeObject)EditorGUI.ObjectField(labelRect, GUIContent.none, resolvedKnowledge as Object, typeof(ScriptableKnowledgeObject), false);

            // Set the type property based on the selected object
            var typeProp = property.FindPropertyRelative("Type");
            if (selectedObject != null)
            {
                typeProp.enumValueIndex = ArrayUtility.IndexOf(typeProp.enumNames, selectedObject.GetType().Name);
                var resolvedConditionProp = property.FindPropertyRelative($"item{typeProp.enumNames[typeProp.enumValueIndex]}");
                resolvedConditionProp.objectReferenceValue = selectedObject;

                // Draw the current value
                var knowledge = selectedObject as IKnowledge;
                Rect valueRect = position;
                valueRect.x = indentStart;
                valueRect.width = indentWidth;
                valueRect.height = lineHeight;
                valueRect.y += lineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.LabelField(valueRect, $"Current value: {knowledge?.GetCurrentValue()}", EditorStyles.helpBox);
            }
            else
            {
                typeProp.enumValueIndex = 0;
            }

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * lineCount;
        }
    }
}