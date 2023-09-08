using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(GlobalKnowledgeValue))]
    public class GlobalKnowledgeValueDrawer : PropertyDrawer
    {
        private const int lineCount = 3;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = position.height / lineCount;

            EditorGUI.BeginProperty(position, label, property);

            label.text = "GK Value";
            var oldWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 100;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            Rect keyRect = new Rect(position.x, position.y, position.width, height);
            EditorGUI.PropertyField(keyRect, property.FindPropertyRelative("key"), GUIContent.none);


            position.y += height;
            Rect enumRect = new Rect(position.x, position.y, position.width, height);
            EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("type"), GUIContent.none);


            position.y += height;
            label.text = "value:";
            EditorGUIUtility.labelWidth = 45;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            Rect valueRect = new Rect(position.x, position.y, position.width, height);

            EditorGUIUtility.labelWidth = oldWidth;



            switch ((GlobalKnowledgeValue.ValueType)property.FindPropertyRelative("type").enumValueIndex)
            {
                case GlobalKnowledgeValue.ValueType.Bool:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("boolValue"), GUIContent.none);
                    break;

                case GlobalKnowledgeValue.ValueType.Float:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("floatValue"), GUIContent.none);
                    break;

                case GlobalKnowledgeValue.ValueType.String:
                    EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("stringValue"), GUIContent.none);
                    break;
            }






            EditorGUI.EndProperty();
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * lineCount;
        }
    }
}