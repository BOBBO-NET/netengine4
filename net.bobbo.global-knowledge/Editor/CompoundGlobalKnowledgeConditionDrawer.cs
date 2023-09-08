using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BobboNet.Knowledge;

namespace BobboNet.Editor.Knowledge
{
    [CustomPropertyDrawer(typeof(CompoundGlobalKnowledgeCondition))]
    public class CompoundGlobalKnowledgeConditionDrawer : PropertyDrawer
    {
        private const int lineCount = 1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var conditions = property.FindPropertyRelative("conditions");
            var operators = property.FindPropertyRelative("operators");

            float height = 0;

            height += EditorGUIUtility.singleLineHeight;
            height += 20;

            int conditionCount = conditions.arraySize;
            int operatorCount = operators.arraySize;
            for (int i = 0; i < conditionCount; i++)
            {
                height += EditorGUI.GetPropertyHeight(conditions.GetArrayElementAtIndex(i), true);
                height += 16;

                if (i < operatorCount)
                {
                    height += EditorGUI.GetPropertyHeight(operators.GetArrayElementAtIndex(i), true) + 16;
                }
            }




            return height;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var conditions = property.FindPropertyRelative("conditions");
            var operators = property.FindPropertyRelative("operators");



            float height = position.y;


            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.DrawRect(position, new Color32(140, 140, 160, 255));

            Rect buttonContainerRect = EditorGUI.PrefixLabel(new Rect(position.x + 10, position.y + 10, position.width - 10, EditorGUIUtility.singleLineHeight),
                new GUIContent(property.displayName), EditorStyles.whiteLabel);


            Rect addButtonRect = new Rect(buttonContainerRect.x,
                buttonContainerRect.y,
                buttonContainerRect.width / 2f,
                EditorGUIUtility.singleLineHeight);
            Rect removeButtonRect = new Rect(buttonContainerRect.x + addButtonRect.width,
                addButtonRect.y,
                buttonContainerRect.width / 2f,
                EditorGUIUtility.singleLineHeight);

            height += buttonContainerRect.height + 20;




            bool addButtonPressed = GUI.Button(addButtonRect, "+", EditorStyles.miniButtonLeft);
            bool removeButtonPressed = GUI.Button(removeButtonRect, "-", EditorStyles.miniButtonRight);

            if (addButtonPressed)
            {
                conditions.arraySize++;
                operators.arraySize = conditions.arraySize - 1;
            }
            else if (removeButtonPressed)
            {
                if (conditions.arraySize > 0)
                {
                    conditions.arraySize--;

                    if (conditions.arraySize > 0)
                    {
                        operators.arraySize = conditions.arraySize - 1;
                    }
                }
            }









            EditorGUI.indentLevel++;


            position = EditorGUI.IndentedRect(position);



            Rect conditionRect;
            Rect operatorRect;

            SerializedProperty currentProperty;
            float tempHeight;



            int conditionCount = conditions.arraySize;
            int operatorCount = operators.arraySize;

            for (int i = 0; i < conditionCount; i++)
            {
                currentProperty = conditions.GetArrayElementAtIndex(i);
                tempHeight = EditorGUI.GetPropertyHeight(currentProperty, true) + 16;

                conditionRect = EditorGUI.IndentedRect(new Rect(position.x, height, position.width, tempHeight));
                EditorGUI.DrawRect(conditionRect, Color.grey);

                conditionRect.y += 8;
                conditionRect.height -= 8;
                conditionRect.width -= 10;
                EditorGUI.PropertyField(conditionRect, currentProperty, true);
                height += tempHeight;

                if (i < operatorCount)
                {
                    currentProperty = operators.GetArrayElementAtIndex(i);
                    tempHeight = EditorGUI.GetPropertyHeight(currentProperty, true) + 16;

                    operatorRect = new Rect(position.x, height, position.width, tempHeight);

                    EditorGUI.DrawRect(operatorRect, new Color32(170, 170, 170, 255));

                    operatorRect.y += 8;
                    operatorRect.height -= 8;
                    operatorRect.width /= 2.5f;
                    EditorGUI.PropertyField(operatorRect, currentProperty, GUIContent.none, true);
                    height += tempHeight;

                }

            }

            EditorGUI.indentLevel--;









            EditorGUI.EndProperty();
        }
    }
}