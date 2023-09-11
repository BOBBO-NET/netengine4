using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;
using System.Linq;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(KnowledgeCondition<>), useForChildren: true)]
    public class KnowledgeConditionGenericDrawer : PropertyDrawer
    {
        private const int lineCount = 1;
        private const int propertiesLineCount = 1;
        private const int boxMargin = 5;
        public bool showConditionProperties;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float lineHeight = base.GetPropertyHeight(property, label);
            var knowledgeCondition = fieldInfo.GetValue(property.serializedObject.targetObject) as IKnowledgeCondition;
            var targetKnowledgeProp = property.FindPropertyRelative("TargetKnowledge");

            EditorGUI.BeginProperty(position, label, property);

            // Draw Object Field
            Rect labelRect = position;
            labelRect.height = lineHeight;
            showConditionProperties = EditorGUI.Foldout(labelRect, showConditionProperties, label);
            // labelRect = EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            labelRect.x += EditorGUIUtility.labelWidth;
            labelRect.width -= EditorGUIUtility.labelWidth;
            float indentStart = labelRect.x;
            float indentWidth = labelRect.width;
            EditorGUI.ObjectField(labelRect, targetKnowledgeProp, GUIContent.none);

            if (showConditionProperties)
            {
                var targetCaseProp = property.FindPropertyRelative("TargetCase");
                IKnowledgeCase selectedCase = null;
                SerializedProperty selectedProp = null;
                string[] arrayOfNames = new string[0];
                float additionalBoxHeight = 0;

                if (knowledgeCondition != null)
                {
                    arrayOfNames = knowledgeCondition.GetCases().Select(knowledgeCase => knowledgeCase.GetName()).ToArray();
                    selectedCase = knowledgeCondition.GetCases().ToArray()[targetCaseProp.intValue];
                    selectedProp = property.FindPropertyRelative(selectedCase.GetName());

                    additionalBoxHeight = base.GetPropertyHeight(selectedProp, label);
                }

                // Calc position for the background
                Rect backgroundRect = position;
                backgroundRect.x += 20;
                backgroundRect.width -= 20;
                backgroundRect.height = (boxMargin * 2) + (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * propertiesLineCount + additionalBoxHeight;
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
                    selectedProp = property.FindPropertyRelative(selectedCase.GetName());
                    EditorGUI.PropertyField(caseValRect, selectedProp);
                }
            }

            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var knowledgeCondition = fieldInfo.GetValue(property.serializedObject.targetObject) as IKnowledgeCondition;
            var targetCaseProp = property.FindPropertyRelative("TargetCase");
            var selectedCase = knowledgeCondition.GetCases().ToArray()[targetCaseProp.intValue];
            var selectedProp = property.FindPropertyRelative(selectedCase.GetName());

            return (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * (lineCount + (showConditionProperties ? propertiesLineCount : 0)) + (showConditionProperties ? base.GetPropertyHeight(selectedProp, label) : 0) + (boxMargin * 2);
        }
    }
}