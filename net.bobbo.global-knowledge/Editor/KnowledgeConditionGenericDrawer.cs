using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;
using System.Linq;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(KnowledgeConditionGeneric), useForChildren: true)]
    public class KnowledgeConditionGenericDrawer : PropertyDrawer
    {
        private const int lineCount = 1;
        private const int propertiesLineCount = 1;
        private const int boxMargin = 5;
        public bool showConditionProperties;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = base.GetPropertyHeight(property, label);
            var knowledgeConditionGeneric = EditorHelper.GetTargetObjectOfProperty(property) as KnowledgeConditionGeneric;
            EditorGUI.BeginProperty(position, label, property);

            Rect labelRect = position;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            showConditionProperties = EditorGUI.Foldout(labelRect, showConditionProperties, label);
            labelRect.x += EditorGUIUtility.labelWidth;
            labelRect.width -= EditorGUIUtility.labelWidth;

            // Draw the field for the target knowledge
            var resolvedCondition = knowledgeConditionGeneric.Get();
            var resolvedTarget = resolvedCondition != null ? resolvedCondition.GetTargetKnowledge() : null;
            ScriptableKnowledgeObject selectedObject = (ScriptableKnowledgeObject)EditorGUI.ObjectField(labelRect, GUIContent.none, resolvedTarget as Object, typeof(ScriptableKnowledgeObject), false);

            // Set the type property based on the selected object
            var typeProp = property.FindPropertyRelative("Type");
            if (selectedObject != null)
            {
                typeProp.enumValueIndex = ArrayUtility.IndexOf(typeProp.enumNames, selectedObject.GetType().Name);
                var resolvedConditionProp = property.FindPropertyRelative($"condition{typeProp.enumNames[typeProp.enumValueIndex]}");
                resolvedConditionProp.FindPropertyRelative("TargetKnowledge").objectReferenceValue = selectedObject;
            }
            else
            {
                typeProp.enumValueIndex = 0;
            }

            if (showConditionProperties && typeProp.enumValueIndex != 0)
            {
                var resolvedConditionProp = property.FindPropertyRelative($"condition{typeProp.enumNames[typeProp.enumValueIndex]}");
                var knowledgeCondition = EditorHelper.GetTargetObjectOfProperty(resolvedConditionProp) as IKnowledgeCondition;
                var targetCaseProp = resolvedConditionProp.FindPropertyRelative("TargetCase");
                IKnowledgeCase selectedCase = null;
                SerializedProperty selectedProp = null;
                string[] arrayOfNames = new string[0];
                float additionalBoxHeight = 0;

                if (knowledgeCondition != null)
                {
                    arrayOfNames = knowledgeCondition.GetCases().Select(knowledgeCase => knowledgeCase.GetName()).ToArray();
                    selectedCase = knowledgeCondition.GetCases().ToArray()[targetCaseProp.intValue];
                    selectedProp = resolvedConditionProp.FindPropertyRelative(selectedCase.GetName());

                    additionalBoxHeight = EditorGUI.GetPropertyHeight(selectedProp, label);
                }

                // Calc position for the background
                Rect backgroundRect = position;
                backgroundRect.x += 20;
                backgroundRect.width -= 20;
                backgroundRect.height = (boxMargin * 2) + (base.GetPropertyHeight(resolvedConditionProp, label) + EditorGUIUtility.standardVerticalSpacing) * propertiesLineCount + additionalBoxHeight;
                backgroundRect.y += lineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.HelpBox(backgroundRect, "", MessageType.None);

                // Calc position for the conditon label
                Rect conditionLabelRect = position;
                conditionLabelRect.x = backgroundRect.x + boxMargin;
                conditionLabelRect.width = backgroundRect.width - (boxMargin * 2);
                conditionLabelRect.height = lineHeight;
                conditionLabelRect.y = backgroundRect.y + boxMargin;

                var conditionLabelContent = new GUIContent("Condition Case");
                Rect caseDropdownRect = EditorGUI.PrefixLabel(conditionLabelRect, GUIUtility.GetControlID(FocusType.Passive), conditionLabelContent);

                // Calc position for case value
                Rect caseValRect = conditionLabelRect;
                caseValRect.y += lineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (knowledgeCondition != null)
                {
                    targetCaseProp.intValue = EditorGUI.Popup(caseDropdownRect, targetCaseProp.intValue, arrayOfNames);

                    selectedCase = knowledgeCondition.GetCases().ToArray()[targetCaseProp.intValue];
                    selectedProp = resolvedConditionProp.FindPropertyRelative(selectedCase.GetName());
                    EditorGUI.PropertyField(caseValRect, selectedProp);
                }
            }

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var typeProp = property.FindPropertyRelative("Type");
            var resolvedConditionProp = property.FindPropertyRelative($"condition{typeProp.enumNames[typeProp.enumValueIndex]}");

            var knowledgeCondition = EditorHelper.GetTargetObjectOfProperty(resolvedConditionProp) as IKnowledgeCondition;
            var targetCaseProp = resolvedConditionProp.FindPropertyRelative("TargetCase");
            var selectedCase = knowledgeCondition.GetCases().ToArray()[targetCaseProp.intValue];
            var selectedProp = resolvedConditionProp.FindPropertyRelative(selectedCase.GetName());

            return (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * (lineCount + (showConditionProperties ? propertiesLineCount : 0)) + (showConditionProperties ? EditorGUI.GetPropertyHeight(selectedProp, label) : 0) + (showConditionProperties ? (boxMargin * 2) : 0);
        }
    }
}