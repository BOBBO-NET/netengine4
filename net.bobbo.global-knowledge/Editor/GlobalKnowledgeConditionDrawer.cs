using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(GlobalKnowledgeCondition))]
    public class GlobalKnowledgeConditionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty conditionTypeProperty = property.FindPropertyRelative("conditionType");
            SerializedProperty nameProperty = property.FindPropertyRelative("name");

            float height = 0;

            height += EditorGUI.GetPropertyHeight(conditionTypeProperty);
            height += EditorGUI.GetPropertyHeight(nameProperty);


            GlobalKnowledgeCondition.ConditionType conditionType = (GlobalKnowledgeCondition.ConditionType)conditionTypeProperty.enumValueIndex;
            switch (conditionType)
            {
                case GlobalKnowledgeCondition.ConditionType.Bool:
                    SerializedProperty boolStateProperty = property.FindPropertyRelative("boolState");

                    height += EditorGUI.GetPropertyHeight(boolStateProperty);
                    break;

                case GlobalKnowledgeCondition.ConditionType.Float:
                    SerializedProperty floatStateProperty = property.FindPropertyRelative("floatState");
                    SerializedProperty floatValueProperty = property.FindPropertyRelative("floatValue");

                    height += EditorGUI.GetPropertyHeight(floatStateProperty);
                    height += EditorGUI.GetPropertyHeight(floatValueProperty);
                    break;

                case GlobalKnowledgeCondition.ConditionType.String:
                    SerializedProperty stringStateProperty = property.FindPropertyRelative("stringState");
                    SerializedProperty stringValueProperty = property.FindPropertyRelative("stringValue");

                    height += EditorGUI.GetPropertyHeight(stringStateProperty);
                    height += EditorGUI.GetPropertyHeight(stringValueProperty);

                    break;
            }



            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty conditionTypeProperty = property.FindPropertyRelative("conditionType");
            SerializedProperty nameProperty = property.FindPropertyRelative("name");



            float height = position.y;

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), conditionTypeProperty);
            height += EditorGUI.GetPropertyHeight(conditionTypeProperty);

            EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), nameProperty);
            height += EditorGUI.GetPropertyHeight(nameProperty);


            GlobalKnowledgeCondition.ConditionType conditionType = (GlobalKnowledgeCondition.ConditionType)conditionTypeProperty.enumValueIndex;
            switch (conditionType)
            {
                case GlobalKnowledgeCondition.ConditionType.Bool:
                    SerializedProperty boolStateProperty = property.FindPropertyRelative("boolState");

                    EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), boolStateProperty);
                    height += EditorGUI.GetPropertyHeight(boolStateProperty);
                    break;

                case GlobalKnowledgeCondition.ConditionType.Float:
                    SerializedProperty floatStateProperty = property.FindPropertyRelative("floatState");
                    SerializedProperty floatValueProperty = property.FindPropertyRelative("floatValue");

                    EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), floatStateProperty);
                    height += EditorGUI.GetPropertyHeight(floatStateProperty);

                    EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), floatValueProperty);
                    height += EditorGUI.GetPropertyHeight(floatValueProperty);
                    break;

                case GlobalKnowledgeCondition.ConditionType.String:
                    SerializedProperty stringStateProperty = property.FindPropertyRelative("stringState");
                    SerializedProperty stringValueProperty = property.FindPropertyRelative("stringValue");

                    EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), stringStateProperty);
                    height += EditorGUI.GetPropertyHeight(stringStateProperty);

                    EditorGUI.PropertyField(new Rect(position.x, height, position.width, EditorGUIUtility.singleLineHeight), stringValueProperty);
                    height += EditorGUI.GetPropertyHeight(stringValueProperty);

                    break;
            }

            EditorGUI.EndProperty();
        }
    }
}